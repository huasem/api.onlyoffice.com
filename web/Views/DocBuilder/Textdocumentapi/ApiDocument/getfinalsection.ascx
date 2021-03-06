<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<h1>
   <span class="hdr">GetFinalSection</span>
</h1>

<h4 class="header-gray" id="GetFinalSection">GetFinalSection() &rarr; {<a href="<%= Url.Action("textdocumentapi/apisection") %>">ApiSection</a>}</h4>
                
<p class="dscr">Get the document final section.</p>                 
                
<h2>Returns:</h2>
<dl class="param-type">
    <dt>Type</dt>
    <dd>
        <a href="<%= Url.Action("textdocumentapi/apisection") %>">ApiSection</a>
    </dd>
</dl>
            
<h2>Example</h2>
<div class="button copy-code">Copy code</div>
<pre>builder.CreateFile("docx");
var oDocument = Api.GetDocument();
var oParagraph, oParaPr, oTextPr, oRun;
var oSection = oDocument.GetFinalSection();
var oHeader = oSection.GetHeader("default", true);
oParagraph = oHeader.GetElement(0);
oParagraph.AddText("This is the text in the default header");
oTextPr = oDocument.GetDefaultTextPr();
oTextPr.SetFontSize(22);
oTextPr.SetLanguage("en-US");
oTextPr.SetFontFamily("Calibri");
oParaPr = oDocument.GetDefaultParaPr();
oParaPr.SetSpacingLine(276, "auto");
oParaPr.SetSpacingAfter(200);
oParagraph = oDocument.GetElement(0);
oParagraph.AddText("This is a text in a paragraph.");
builder.SaveFile("docx", "GetFinalSection.docx");
builder.CloseFile();</pre>

<h2>Resulting document</h2>
<iframe class="docbuilder_resulting_docs" src="https://help.onlyoffice.com/products/files/doceditor.aspx?fileid=4887254&doc=QWhnZ29uMHE4VTBwWGpJWTBURytzenVsSEJrdkdmekd3Z1BIV3BCKzNaUT0_IjQ4ODcyNTQi0&action=embedded" frameborder="0" scrolling="no" allowtransparency></iframe>
