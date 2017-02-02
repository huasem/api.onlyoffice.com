﻿<%@ Page
    Title=""
    Language="C#"
    MasterPageFile="~/Views/Shared/Site.Master"
    Inherits="System.Web.Mvc.ViewPage"
    ContentType="text/html" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Advanced parameters
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1>
        <span class="hdr">Advanced parameters</span>
    </h1>

    <p class="dscr">The parameters, which can be changed for ONLYOFFICE Document Server, can be subdivided into the following main sections:</p>

    <a href="<%= Url.Action("config/") %>"><b>config</b></a> - allows to change the platform type used, document display size (width and height) and type of the document opened;
    <ul>
        <li>
            <a href="<%= Url.Action("config/document") %>"><b>document</b></a> - contains all the parameters pertaining to the document (title, url, file type, etc.);
            <ul>
                <li>
                    <a href="<%= Url.Action("config/document/info") %>"><b>info</b></a> - contains additional parameters for the document (document author, folder where the document is stored, creation date, sharing settings);
                </li>
                <li>
                    <a href="<%= Url.Action("config/document/permissions") %>"><b>permissions</b></a> - defines whether the document can be edited and downloaded or not;
                </li>
            </ul>
        </li>
        <li>
            <a href="<%= Url.Action("config/editor") %>"><b>editorConfig</b></a> - defines parameters pertaining to the editor interface: opening mode (viewer or editor), interface language, additional buttons, etc.);
            <ul>
                <li>
                    <a href="<%= Url.Action("config/editor/customization") %>"><b>customization</b></a> - allows to customize the editor interface so that it looked like your other products (if there are any) and change the presence or absence of the additional buttons, links, change logos and editor owner details;
                </li>
                <li>
                    <a href="<%= Url.Action("config/editor/embedded") %>"><b>embedded</b></a> - is used for the embedded document type only and allows to change the behavior of the buttons used to control the embedded mode;
                </li>
                <li>
                    <a href="<%= Url.Action("config/editor/plugins") %>"><b>plugins</b></a> - is used to connect the necessary <a href="<%= Url.Action("basic", "plugin") %>">plugins</a> to your Document Server, so that they become visible to all document editor users;
                </li>
            </ul>
        </li>
        <li>
            <a href="<%= Url.Action("config/events") %>"><b>events</b></a> - is the list of special events called when some action is applied to the document (when it is loaded, modified, etc.).
        </li>
    </ul>

    <p>The complete <em>config</em> with all the additional parameters looks the following way:</p>

    <pre>
config = {
    "document": {
        "fileType": "docx",
        "info": {
            "author": "John Smith",
            "created": "2010-07-07 3:46 PM",
            "folder": "Example Files",
            "sharingSettings": [
                {
                    "permissions": "Full Access",
                    "user": "John Smith",
                },
                {
                    "permissions": "Read Only",
                    "user": "Kate Cage",
                },
                ...
            ],
        },
        "key": "Khirz6zTPdfd7",
        "permissions": {
            "download": true,
            "edit": true,
            "print": true,
            "review": true,
        },
        "title": "Example Document Title.docx",
        "url": "http://example.com/url-to-example-document.docx",
    },
    "documentType": "text",
    "editorConfig": {
        "callbackUrl": "http://example.com/url-to-callback.ashx",
        "createUrl": "http://example.com/url-to-create-document/",
        "customization": {
            "chat": true,
            "comments": true,
            "compactToolbar": false,
            "customer": {
                "address": "My City, 123a-45",
                "info": "Some additional information",
                "logo": "http://example.com/logo-big.png",
                "mail": "john@example.com",
                "name": "John Smith and Co.",
                "www": "example.com",
            },
            "feedback": {
                "url": "http://example.com",
                "visible": true,
            },
            "goback": {
                "text": "Go to Documents",
                "url": "http://example.com",
            },
            "logo": {
                "image": "http://example.com/logo.png",
                "imageEmbedded": "http://example.com/logo_em.png",
                "url": "http://example.com",
            },
            "zoom": 100,
        },
        "embedded": {
            "embedUrl": "http://example.com/embedded?doc=exampledocument1.docx",
            "fullscreenUrl": "http://example.com/embedded?doc=exampledocument1.docx#fullscreen",
            "saveUrl": "http://example.com/download?doc=exampledocument1.docx",
            "shareUrl": "http://example.com/view?doc=exampledocument1.docx",
            "toolbarDocked": "top",
        },
        "lang": "en-US",
        "mode": "edit",
        "plugins": {
             "pluginsData": [
                 "plugin1/config.json",
                 "plugin2/config.json",
                 ...
             ],
             "url": "http://example.com/plugins/",
        },
        "recent": [
            {
                "folder": "Example Files",
                "title": "exampledocument1.docx",
                "url": "http://example.com/exampledocument1.docx",
            },
            {
                "folder": "Example Files",
                "title": "exampledocument2.docx",
                "url": "http://example.com/exampledocument2.docx",
            },
            ...
        ],
        "user": {
            "id": "78e1e841",
            "name": "John Smith",
        },
    },
    "events": {
        "onCollaborativeChanges": onCollaborativeChanges,
        "onDocumentStateChange": onDocumentStateChange,
        "onDownloadAs": onDownloadAs,
        "onError": onError,
        "onReady": onReady,
        "onRequestEditRights": onRequestEditRights,
        "onRequestHistory": onRequestHistory,
        "onRequestHistoryData": onRequestHistoryData,
        "onRequestHistoryClose": onRequestHistoryClose,
    },
    "height": "100%",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.e30.t-IDcSemACt8x4iTMCda8Yhe3iZaWbvV5XKSTbuAn0M",
    "type": "desktop",
    "width": "100%",
};
</pre>
</asp:Content>
