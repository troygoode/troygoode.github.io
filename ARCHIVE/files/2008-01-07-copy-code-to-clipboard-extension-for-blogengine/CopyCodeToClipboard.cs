using System;
using System.Text.RegularExpressions;
using BlogEngine.Core;
using BlogEngine.Core.Web.Controls;

[Extension("Adds a link to syntax highlighted code to copy it to the clipboard.", "1.0", @"<a href=""http://www.squaredroot.com"">Troy Goode</a>")]
public class CopyCodeToClipboard
{

	private ExtensionSettings settings;

	public CopyCodeToClipboard()
	{

		Post.Serving += new EventHandler<ServingEventArgs>(Post_PostServing);
		Post.Saving += new EventHandler<SavedEventArgs>(Post_Saving);

		ExtensionSettings initialSettings = new ExtensionSettings(GetType().Name);
		initialSettings.Help = "This extension is written to work with the <a href=\"http://lvildosola.blogspot.com/2007/02/code-snippet-plugin-for-windows-live.html\">Code Snippet Plugin for Windows Live Writer</a> and will not work with other syntax highlighting tools.";
		initialSettings.AddParameter( "copyText", "Text for 'Copy to Clipboard' button?", 255, true );
			initialSettings.AddValue( "copyText", "Copy To Clipboard" );
		initialSettings.AddParameter( "popupText", "Text for 'View in Popup Window' button?", 255, true );
			initialSettings.AddValue( "popupText", "View Plain" );
		initialSettings.AddParameter("aboveBelow", "Display above or below code?", 5, true);
			initialSettings.AddValue("aboveBelow", "Below");
		initialSettings.AddParameter( "style", "Any additional styling?", int.MaxValue, false );
			initialSettings.AddValue( "style", "" );
		initialSettings.AddParameter( "flashFile", "Path to '_clipboard.swf' Flash file?", 255, true );
			initialSettings.AddValue( "flashFile", "~/_clipboard.swf" );
		initialSettings.IsScalar = true;
		ExtensionManager.ImportSettings(initialSettings);

		settings = ExtensionManager.GetSettings(GetType().Name);

	}

	private void Post_Saving(object sender, SavedEventArgs e)
	{
		//### executing here will only run the code once per post, but will modify the post itself
		//InsertCopyCodeLink(e);
	}

	private void Post_PostServing(object sender, ServingEventArgs e)
	{
		//### executing here will execute the code on every iteration, but will not modify your post
		InsertCopyCodeLink(e);
	}

	private void InsertCopyCodeLink( ServingEventArgs e)
	{
		if( !string.IsNullOrEmpty(e.Body) )
		{

			//### find code-div
			string postID = Guid.NewGuid().ToString().Replace( "{", "" ).Replace( "}", "" ).Replace( "-", "" );
			string toFind = "<div class=\"csharpcode-wrapper\">";
			int index = e.Body.IndexOf(toFind);
			while( index != -1 )
			{

				//### grab code out of code-div
				int end = e.Body.IndexOf( "</div>", index ); //### the first </div> should be the end of "csharpcode"
				end = e.Body.IndexOf( "</div>", end ); //### the next </div> should be the end of "csharpcode-wrapper"
				string code = e.Body.Substring(index, end - index);

				//### parse code out of code-div
				int codeStart = code.IndexOf("<pre ");
				int codeEnd = code.LastIndexOf("</pre>") + 6;
				code = code.Substring(codeStart, codeEnd - codeStart);
				code = Regex.Replace( code, @"<(.|\n)*?>", string.Empty ); //### strip html
				code = code.Replace( "&#160;", "" ); //### remove unnecessary &#160;s from the blank lines
				code = code.Replace( "&nbsp;", " " ); //### convert &nbsp;s to spaces
				code = Regex.Replace( code, @"^(\s*)(\d+): ", "" ); //### remove line numbers on first line
				code = Regex.Replace( code, @"(\n)(\s*)(\d+): ", "\n" );  //### remove line numbers on subsequent lines

				//### create copy link
				string insertScript = @"
					<script type=""text/javascript"">
						var copyToClipboard@INDEX = CopyToClipboard_Strip('@CODE');
					</script>";
				string insertDiv = @"<div class=""CopyToClipboard"" style=""@STYLE""><div><a href=""javascript:void(0);"" onclick=""CopyToClipboard_ViewPlain(copyToClipboard@INDEX);"">@POPUPTEXT</a> | <a href=""javascript:void(0);"" onclick=""CopyToClipboard_Copy(copyToClipboard@INDEX);"">@COPYTEXT</a></div></div>";

				//### set values for copy link and insert above/below code
				string insert = insertDiv + OutputCommonMethods() + insertScript;
				insert = insert.Replace( "@STYLE", settings.GetSingleValue("style") );
				insert = insert.Replace( "@POPUPTEXT", settings.GetSingleValue("popupText") );
				insert = insert.Replace( "@COPYTEXT", settings.GetSingleValue("copyText") );
				insert = insert.Replace( "@INDEX", postID + "_" + index.ToString() ); //### use index of code-div as a unique ID to allow multiple code-divs on this post
				insert = insert.Replace( "@CODE", code.Replace( "\\", "\\\\" ).Replace( "'", "\\'" ).Replace( "\r\n", "\\r\\n" ).Replace( "\\r\\n\\r\\n\\r\\n", "\\r\\n" ) );
				if( settings.GetSingleValue("aboveBelow").ToLower() == "above" )
					e.Body = e.Body.Insert( index, insert );
				else
					e.Body = e.Body.Insert( e.Body.IndexOf( "</div>", end + 1 ) + 6, insert );

				//### prep index to find next code-div
				index = index + insert.Length + 1; //### ensure we don't find this same code-div again
				if( index > e.Body.Length ) break;
				index = e.Body.IndexOf( toFind, index ); //### find any other code divs

			}
		}
	}

	private string OutputCommonMethods()
	{

		//### only output the following once per page
		if( System.Web.HttpContext.Current.Items["CopyToClipboard_JSOutput"] == null )
		{
			//### add shared javascript
			string flashPath = System.Web.VirtualPathUtility.ToAbsolute(settings.GetSingleValue("flashFile"));
			string commonMethods = @"
				<div id=""CopyToClipboard_Hidden"" style=""display:none;""></div>
				<div id=""CopyToClipboard_FlashContainer""></div>
                <script type=""text/javascript"">

					function CopyToClipboard_Strip( text ){
						text = text.replace( /&nbsp;/g, ' ' );
						text = text.replace( /&quot;/g, '""' );
						text = text.replace( /&#39;/g, '""' );
						text = text.replace( /&amp;/g, '&' );
						text = text.replace( /&lt;/g, String.fromCharCode(60) );
						text = text.replace( /&gt;/g, String.fromCharCode(62) );
						return text;
					}

                    function CopyToClipboard_Copy( text ){

						//### get reference to utility div
						var ele = document.getElementById('CopyToClipboard_Hidden');

						//### the following taken from: http://webchicanery.com/2006/11/14/clipboard-copy-javascript/
						if (false && window.clipboardData) {
							window.clipboardData.setData( ""Text"", text );
						} else {
							document.getElementById('CopyToClipboard_FlashContainer').innerHTML = '';
							var divinfo = '<embed id=""CopyToClipboard_FlashFile"" src=""" + flashPath + @""" FlashVars=""clipboard=' + encodeURIComponent(text) + '"" width=""0"" height=""0"" type=""application/x-shockwave-flash""></embed>';
							document.getElementById('CopyToClipboard_FlashContainer').innerHTML = divinfo;
						}

                    }

					function CopyToClipboard_ViewPlain( text ){
						var win = window.open( '', 'CopyToClipboard_Window', 'width=480, height=480, toolbar=no, menubar=no, scrollbars=auto, resizable=yes, location=no, directories=no, status=no' );
						win.document.write( '<html><head><title>Code</title><body style=""margin:0;padding:0;""><textarea style=""width:100%;height:100%;border:0;"">' + text + '</textarea></body></html>' );
					}

                </script>
            ";			
			System.Web.HttpContext.Current.Items["CopyToClipboard_JSOutput"] = true;
			return commonMethods;
		}
		else
			return "";

	}

}