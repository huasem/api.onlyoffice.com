/*
 *
 * (c) Copyright Ascensio System Limited 2010-2017
 *
 * This program is freeware. You can redistribute it and/or modify it under the terms of the GNU 
 * General Public License (GPL) version 3 as published by the Free Software Foundation (https://www.gnu.org/copyleft/gpl.html). 
 * In accordance with Section 7(a) of the GNU GPL its Section 15 shall be amended to the effect that 
 * Ascensio System SIA expressly excludes the warranty of non-infringement of any third-party rights.
 *
 * THIS PROGRAM IS DISTRIBUTED WITHOUT ANY WARRANTY; WITHOUT EVEN THE IMPLIED WARRANTY OF MERCHANTABILITY OR
 * FITNESS FOR A PARTICULAR PURPOSE. For more details, see GNU GPL at https://www.gnu.org/copyleft/gpl.html
 *
 * You can contact Ascensio System SIA by email at sales@onlyoffice.com
 *
 * The interactive user interfaces in modified source and object code versions of ONLYOFFICE must display 
 * Appropriate Legal Notices, as required under Section 5 of the GNU GPL version 3.
 *
 * Pursuant to Section 7 § 3(b) of the GNU GPL you must retain the original ONLYOFFICE logo which contains 
 * relevant author attributions when distributing the software. If the display of the logo in its graphic 
 * form is not reasonably feasible for technical reasons, you must include the words "Powered by ONLYOFFICE" 
 * in every copy of the program you distribute. 
 * Pursuant to Section 7 § 3(e) we decline to grant you any rights under trademark law for use of our trademarks.
 *
*/


using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using ASC.Api.Web.Help.DocumentGenerator;
using ASC.Api.Web.Help.Helpers;
using HtmlAgilityPack;
using log4net;

namespace ASC.Api.Web.Help.Controllers
{
    [Redirect]
    public class DocBuilderController : AsyncController
    {
        #region actions

        private readonly string[] _actionMap = new[]
            {
                "Basic",
                "gettingstarted",
                "csharpexample",
                "nodejsexample",
                "phpexample",
                "rubyexample",
                "integratingdocumentbuilder",
                "integrationapi",
                "integrationapi/usingdocbuilderfile",
                "integrationapi/cdocbuilder",
                "integrationapi/cdocbuilder/closefile",
                "integrationapi/cdocbuilder/createfile",
                "integrationapi/cdocbuilder/dispose",
                "integrationapi/cdocbuilder/executecommand",
                "integrationapi/cdocbuilder/initialize",
                "integrationapi/cdocbuilder/openfile",
                "integrationapi/cdocbuilder/run",
                "integrationapi/cdocbuilder/runtexta",
                "integrationapi/cdocbuilder/runtextw",
                "integrationapi/cdocbuilder/savefile",
                "integrationapi/cdocbuilder/setproperty",
                "integrationapi/cdocbuilder/setpropertyw",
                "integrationapi/cdocbuilder/settmpfolder",
                "integrationapi/globalvariable",
                "integrationapi/arguments",
                "textdocumentapi",
                "textdocumentapi/api",
                "textdocumentapi/api/createblipfill",
                "textdocumentapi/api/createchart",
                "textdocumentapi/api/creategradientstop",
                "textdocumentapi/api/createimage",
                "textdocumentapi/api/createlineargradientfill",
                "textdocumentapi/api/createnofill",
                "textdocumentapi/api/createparagraph",
                "textdocumentapi/api/createpatternfill",
                "textdocumentapi/api/createpresetcolor",
                "textdocumentapi/api/createradialgradientfill",
                "textdocumentapi/api/creatergbcolor",
                "textdocumentapi/api/createrun",
                "textdocumentapi/api/createschemecolor",
                "textdocumentapi/api/createshape",
                "textdocumentapi/api/createsolidfill",
                "textdocumentapi/api/createstroke",
                "textdocumentapi/api/createtable",
                "textdocumentapi/api/getdocument",
                "textdocumentapi/apichart",
                "textdocumentapi/apichart/getclasstype",
                "textdocumentapi/apichart/sethoraxistitle",
                "textdocumentapi/apichart/setlegendpos",
                "textdocumentapi/apichart/setshowdatalabels",
                "textdocumentapi/apichart/settitle",
                "textdocumentapi/apichart/setveraxistitle",
                "textdocumentapi/apidocument",
                "textdocumentapi/apidocument/addelement",
                "textdocumentapi/apidocument/createnumbering",
                "textdocumentapi/apidocument/createsection",
                "textdocumentapi/apidocument/createstyle",
                "textdocumentapi/apidocument/getclasstype",
                "textdocumentapi/apidocument/getcommentsreport",
                "textdocumentapi/apidocument/getdefaultparapr",
                "textdocumentapi/apidocument/getdefaultstyle",
                "textdocumentapi/apidocument/getdefaulttextpr",
                "textdocumentapi/apidocument/getelement",
                "textdocumentapi/apidocument/getelementscount",
                "textdocumentapi/apidocument/getfinalsection",
                "textdocumentapi/apidocument/getreviewreport",
                "textdocumentapi/apidocument/getstyle",
                "textdocumentapi/apidocument/push",
                "textdocumentapi/apidocument/removeallelements",
                "textdocumentapi/apidocument/removeelement",
                "textdocumentapi/apidocument/setevenandoddhdrftr",
                "textdocumentapi/apidocumentcontent",
                "textdocumentapi/apidocumentcontent/addelement",
                "textdocumentapi/apidocumentcontent/getclasstype",
                "textdocumentapi/apidocumentcontent/getelement",
                "textdocumentapi/apidocumentcontent/getelementscount",
                "textdocumentapi/apidocumentcontent/push",
                "textdocumentapi/apidocumentcontent/removeallelements",
                "textdocumentapi/apidocumentcontent/removeelement",
                "textdocumentapi/apidrawing",
                "textdocumentapi/apidrawing/getclasstype",
                "textdocumentapi/apidrawing/setdistances",
                "textdocumentapi/apidrawing/sethoralign",
                "textdocumentapi/apidrawing/sethorposition",
                "textdocumentapi/apidrawing/setsize",
                "textdocumentapi/apidrawing/setveralign",
                "textdocumentapi/apidrawing/setverposition",
                "textdocumentapi/apidrawing/setwrappingstyle",
                "textdocumentapi/apifill",
                "textdocumentapi/apifill/getclasstype",
                "textdocumentapi/apigradientstop",
                "textdocumentapi/apigradientstop/getclasstype",
                "textdocumentapi/apiimage",
                "textdocumentapi/apiimage/getclasstype",
                "textdocumentapi/apinumbering",
                "textdocumentapi/apinumbering/getclasstype",
                "textdocumentapi/apinumbering/getlevel",
                "textdocumentapi/apinumberinglevel",
                "textdocumentapi/apinumberinglevel/getclasstype",
                "textdocumentapi/apinumberinglevel/getlevelindex",
                "textdocumentapi/apinumberinglevel/getnumbering",
                "textdocumentapi/apinumberinglevel/getparapr",
                "textdocumentapi/apinumberinglevel/gettextpr",
                "textdocumentapi/apinumberinglevel/setcustomtype",
                "textdocumentapi/apinumberinglevel/setrestart",
                "textdocumentapi/apinumberinglevel/setstart",
                "textdocumentapi/apinumberinglevel/setsuff",
                "textdocumentapi/apinumberinglevel/settemplatetype",
                "textdocumentapi/apiparagraph",
                "textdocumentapi/apiparagraph/addcolumnbreak",
                "textdocumentapi/apiparagraph/adddrawing",
                "textdocumentapi/apiparagraph/addelement",
                "textdocumentapi/apiparagraph/addlinebreak",
                "textdocumentapi/apiparagraph/addpagebreak",
                "textdocumentapi/apiparagraph/addpagenumber",
                "textdocumentapi/apiparagraph/addpagescount",
                "textdocumentapi/apiparagraph/addtabstop",
                "textdocumentapi/apiparagraph/addtext",
                "textdocumentapi/apiparagraph/getclasstype",
                "textdocumentapi/apiparagraph/getelement",
                "textdocumentapi/apiparagraph/getelementscount",
                "textdocumentapi/apiparagraph/getnumbering",
                "textdocumentapi/apiparagraph/getparagraphmarktextpr",
                "textdocumentapi/apiparagraph/getparapr",
                "textdocumentapi/apiparagraph/removeallelements",
                "textdocumentapi/apiparagraph/removeelement",
                "textdocumentapi/apiparagraph/setbetweenborder",
                "textdocumentapi/apiparagraph/setbottomborder",
                "textdocumentapi/apiparagraph/setcontextualspacing",
                "textdocumentapi/apiparagraph/setindfirstline",
                "textdocumentapi/apiparagraph/setindleft",
                "textdocumentapi/apiparagraph/setindright",
                "textdocumentapi/apiparagraph/setjc",
                "textdocumentapi/apiparagraph/setkeeplines",
                "textdocumentapi/apiparagraph/setkeepnext",
                "textdocumentapi/apiparagraph/setleftborder",
                "textdocumentapi/apiparagraph/setnumbering",
                "textdocumentapi/apiparagraph/setnumpr",
                "textdocumentapi/apiparagraph/setpagebreakbefore",
                "textdocumentapi/apiparagraph/setrightborder",
                "textdocumentapi/apiparagraph/setshd",
                "textdocumentapi/apiparagraph/setspacingafter",
                "textdocumentapi/apiparagraph/setspacingbefore",
                "textdocumentapi/apiparagraph/setspacingline",
                "textdocumentapi/apiparagraph/setstyle",
                "textdocumentapi/apiparagraph/settabs",
                "textdocumentapi/apiparagraph/settopborder",
                "textdocumentapi/apiparagraph/setwidowcontrol",
                "textdocumentapi/apiparapr",
                "textdocumentapi/apiparapr/getclasstype",
                "textdocumentapi/apiparapr/setbetweenborder",
                "textdocumentapi/apiparapr/setbottomborder",
                "textdocumentapi/apiparapr/setcontextualspacing",
                "textdocumentapi/apiparapr/setindfirstline",
                "textdocumentapi/apiparapr/setindleft",
                "textdocumentapi/apiparapr/setindright",
                "textdocumentapi/apiparapr/setjc",
                "textdocumentapi/apiparapr/setkeeplines",
                "textdocumentapi/apiparapr/setkeepnext",
                "textdocumentapi/apiparapr/setleftborder",
                "textdocumentapi/apiparapr/setnumpr",
                "textdocumentapi/apiparapr/setpagebreakbefore",
                "textdocumentapi/apiparapr/setrightborder",
                "textdocumentapi/apiparapr/setshd",
                "textdocumentapi/apiparapr/setspacingafter",
                "textdocumentapi/apiparapr/setspacingbefore",
                "textdocumentapi/apiparapr/setspacingline",
                "textdocumentapi/apiparapr/setstyle",
                "textdocumentapi/apiparapr/settabs",
                "textdocumentapi/apiparapr/settopborder",
                "textdocumentapi/apiparapr/setwidowcontrol",
                "textdocumentapi/apipresetcolor",
                "textdocumentapi/apipresetcolor/getclasstype",
                "textdocumentapi/apirgbcolor",
                "textdocumentapi/apirgbcolor/getclasstype",
                "textdocumentapi/apirun",
                "textdocumentapi/apirun/addcolumnbreak",
                "textdocumentapi/apirun/adddrawing",
                "textdocumentapi/apirun/addlinebreak",
                "textdocumentapi/apirun/addpagebreak",
                "textdocumentapi/apirun/addtabstop",
                "textdocumentapi/apirun/addtext",
                "textdocumentapi/apirun/clearcontent",
                "textdocumentapi/apirun/getclasstype",
                "textdocumentapi/apirun/gettextpr",
                "textdocumentapi/apirun/setbold",
                "textdocumentapi/apirun/setcaps",
                "textdocumentapi/apirun/setcolor",
                "textdocumentapi/apirun/setdoublestrikeout",
                "textdocumentapi/apirun/setfontfamily",
                "textdocumentapi/apirun/setfontsize",
                "textdocumentapi/apirun/sethighlight",
                "textdocumentapi/apirun/setitalic",
                "textdocumentapi/apirun/setlanguage",
                "textdocumentapi/apirun/setposition",
                "textdocumentapi/apirun/setshd",
                "textdocumentapi/apirun/setsmallcaps",
                "textdocumentapi/apirun/setspacing",
                "textdocumentapi/apirun/setstrikeout",
                "textdocumentapi/apirun/setstyle",
                "textdocumentapi/apirun/setunderline",
                "textdocumentapi/apirun/setvertalign",
                "textdocumentapi/apischemecolor",
                "textdocumentapi/apischemecolor/getclasstype",
                "textdocumentapi/apisection",
                "textdocumentapi/apisection/getclasstype",
                "textdocumentapi/apisection/getfooter",
                "textdocumentapi/apisection/getheader",
                "textdocumentapi/apisection/removefooter",
                "textdocumentapi/apisection/removeheader",
                "textdocumentapi/apisection/setequalcolumns",
                "textdocumentapi/apisection/setfooterdistance",
                "textdocumentapi/apisection/setheaderdistance",
                "textdocumentapi/apisection/setnotequalcolumns",
                "textdocumentapi/apisection/setpagemargins",
                "textdocumentapi/apisection/setpagesize",
                "textdocumentapi/apisection/settitlepage",
                "textdocumentapi/apisection/settype",
                "textdocumentapi/apishape",
                "textdocumentapi/apishape/getclasstype",
                "textdocumentapi/apishape/getdoccontent",
                "textdocumentapi/apishape/setverticaltextalign",
                "textdocumentapi/apistroke",
                "textdocumentapi/apistroke/getclasstype",
                "textdocumentapi/apistyle",
                "textdocumentapi/apistyle/getclasstype",
                "textdocumentapi/apistyle/getconditionaltablestyle",
                "textdocumentapi/apistyle/getname",
                "textdocumentapi/apistyle/getparapr",
                "textdocumentapi/apistyle/gettablecellpr",
                "textdocumentapi/apistyle/gettablepr",
                "textdocumentapi/apistyle/gettablerowpr",
                "textdocumentapi/apistyle/gettextpr",
                "textdocumentapi/apistyle/gettype",
                "textdocumentapi/apistyle/setbasedon",
                "textdocumentapi/apistyle/setname",
                "textdocumentapi/apitable",
                "textdocumentapi/apitable/addcolumn",
                "textdocumentapi/apitable/addrow",
                "textdocumentapi/apitable/getclasstype",
                "textdocumentapi/apitable/getrow",
                "textdocumentapi/apitable/getrowscount",
                "textdocumentapi/apitable/mergecells",
                "textdocumentapi/apitable/removecolumn",
                "textdocumentapi/apitable/removerow",
                "textdocumentapi/apitable/setcellspacing",
                "textdocumentapi/apitable/setjc",
                "textdocumentapi/apitable/setshd",
                "textdocumentapi/apitable/setstyle",
                "textdocumentapi/apitable/setstylecolbandsize",
                "textdocumentapi/apitable/setstylerowbandsize",
                "textdocumentapi/apitable/settableborderbottom",
                "textdocumentapi/apitable/settableborderinsideh",
                "textdocumentapi/apitable/settableborderinsidev",
                "textdocumentapi/apitable/settableborderleft",
                "textdocumentapi/apitable/settableborderright",
                "textdocumentapi/apitable/settablebordertop",
                "textdocumentapi/apitable/settablecellmarginbottom",
                "textdocumentapi/apitable/settablecellmarginleft",
                "textdocumentapi/apitable/settablecellmarginright",
                "textdocumentapi/apitable/settablecellmargintop",
                "textdocumentapi/apitable/settableind",
                "textdocumentapi/apitable/settablelayout",
                "textdocumentapi/apitable/settablelook",
                "textdocumentapi/apitable/setwidth",
                "textdocumentapi/apitablecell",
                "textdocumentapi/apitablecell/getclasstype",
                "textdocumentapi/apitablecell/getcontent",
                "textdocumentapi/apitablecell/setcellborderbottom",
                "textdocumentapi/apitablecell/setcellborderleft",
                "textdocumentapi/apitablecell/setcellborderright",
                "textdocumentapi/apitablecell/setcellbordertop",
                "textdocumentapi/apitablecell/setcellmarginbottom",
                "textdocumentapi/apitablecell/setcellmarginleft",
                "textdocumentapi/apitablecell/setcellmarginright",
                "textdocumentapi/apitablecell/setcellmargintop",
                "textdocumentapi/apitablecell/setnowrap",
                "textdocumentapi/apitablecell/setshd",
                "textdocumentapi/apitablecell/settextdirection",
                "textdocumentapi/apitablecell/setverticalalign",
                "textdocumentapi/apitablecell/setwidth",
                "textdocumentapi/apitablecellpr",
                "textdocumentapi/apitablecellpr/getclasstype",
                "textdocumentapi/apitablecellpr/setcellborderbottom",
                "textdocumentapi/apitablecellpr/setcellborderleft",
                "textdocumentapi/apitablecellpr/setcellborderright",
                "textdocumentapi/apitablecellpr/setcellbordertop",
                "textdocumentapi/apitablecellpr/setcellmarginbottom",
                "textdocumentapi/apitablecellpr/setcellmarginleft",
                "textdocumentapi/apitablecellpr/setcellmarginright",
                "textdocumentapi/apitablecellpr/setcellmargintop",
                "textdocumentapi/apitablecellpr/setnowrap",
                "textdocumentapi/apitablecellpr/setshd",
                "textdocumentapi/apitablecellpr/settextdirection",
                "textdocumentapi/apitablecellpr/setverticalalign",
                "textdocumentapi/apitablecellpr/setwidth",
                "textdocumentapi/apitablepr",
                "textdocumentapi/apitablepr/getclasstype",
                "textdocumentapi/apitablepr/setcellspacing",
                "textdocumentapi/apitablepr/setjc",
                "textdocumentapi/apitablepr/setshd",
                "textdocumentapi/apitablepr/setstylecolbandsize",
                "textdocumentapi/apitablepr/setstylerowbandsize",
                "textdocumentapi/apitablepr/settableborderbottom",
                "textdocumentapi/apitablepr/settableborderinsideh",
                "textdocumentapi/apitablepr/settableborderinsidev",
                "textdocumentapi/apitablepr/settableborderleft",
                "textdocumentapi/apitablepr/settableborderright",
                "textdocumentapi/apitablepr/settablebordertop",
                "textdocumentapi/apitablepr/settablecellmarginbottom",
                "textdocumentapi/apitablepr/settablecellmarginleft",
                "textdocumentapi/apitablepr/settablecellmarginright",
                "textdocumentapi/apitablepr/settablecellmargintop",
                "textdocumentapi/apitablepr/settableind",
                "textdocumentapi/apitablepr/settablelayout",
                "textdocumentapi/apitablepr/setwidth",
                "textdocumentapi/apitablerow",
                "textdocumentapi/apitablerow/getcell",
                "textdocumentapi/apitablerow/getcellscount",
                "textdocumentapi/apitablerow/getclasstype",
                "textdocumentapi/apitablerow/setheight",
                "textdocumentapi/apitablerow/settableheader",
                "textdocumentapi/apitablerowpr",
                "textdocumentapi/apitablerowpr/getclasstype",
                "textdocumentapi/apitablerowpr/setheight",
                "textdocumentapi/apitablerowpr/settableheader",
                "textdocumentapi/apitablestylepr",
                "textdocumentapi/apitablestylepr/getclasstype",
                "textdocumentapi/apitablestylepr/getparapr",
                "textdocumentapi/apitablestylepr/gettablecellpr",
                "textdocumentapi/apitablestylepr/gettablepr",
                "textdocumentapi/apitablestylepr/gettablerowpr",
                "textdocumentapi/apitablestylepr/gettextpr",
                "textdocumentapi/apitablestylepr/gettype",
                "textdocumentapi/apitextpr",
                "textdocumentapi/apitextpr/getclasstype",
                "textdocumentapi/apitextpr/setbold",
                "textdocumentapi/apitextpr/setcaps",
                "textdocumentapi/apitextpr/setcolor",
                "textdocumentapi/apitextpr/setdoublestrikeout",
                "textdocumentapi/apitextpr/setfontfamily",
                "textdocumentapi/apitextpr/setfontsize",
                "textdocumentapi/apitextpr/sethighlight",
                "textdocumentapi/apitextpr/setitalic",
                "textdocumentapi/apitextpr/setlanguage",
                "textdocumentapi/apitextpr/setposition",
                "textdocumentapi/apitextpr/setshd",
                "textdocumentapi/apitextpr/setsmallcaps",
                "textdocumentapi/apitextpr/setspacing",
                "textdocumentapi/apitextpr/setstrikeout",
                "textdocumentapi/apitextpr/setstyle",
                "textdocumentapi/apitextpr/setunderline",
                "textdocumentapi/apitextpr/setvertalign",
                "textdocumentapi/apiunicolor",
                "textdocumentapi/apiunicolor/getclasstype",
                "textdocumentapi/apiunsupported",
                "textdocumentapi/apiunsupported/getclasstype",
                "spreadsheetapi",
                "spreadsheetapi/api",
                "spreadsheetapi/api/createblipfill",
                "spreadsheetapi/api/createbullet",
                "spreadsheetapi/api/createcolorbyname",
                "spreadsheetapi/api/createcolorfromrgb",
                "spreadsheetapi/api/createbullet",
                "spreadsheetapi/api/creategradientstop",
                "spreadsheetapi/api/createlineargradientfill",
                "spreadsheetapi/api/createnofill",
                "spreadsheetapi/api/createnumbering",
                "spreadsheetapi/api/createparagraph",
                "spreadsheetapi/api/createpatternfill",
                "spreadsheetapi/api/createpresetcolor",
                "spreadsheetapi/api/createradialgradientfill",
                "spreadsheetapi/api/creatergbcolor",
                "spreadsheetapi/api/createrun",
                "spreadsheetapi/api/createschemecolor",
                "spreadsheetapi/api/createsolidfill",
                "spreadsheetapi/api/createstroke",
                "spreadsheetapi/api/getactivesheet",
                "spreadsheetapi/api/getthemescolors",
                "spreadsheetapi/api/setthemecolors",
                "spreadsheetapi/apibullet",
                "spreadsheetapi/apibullet/getclasstype",
                "spreadsheetapi/apichart",
                "spreadsheetapi/apichart/getclasstype",
                "spreadsheetapi/apichart/sethoraxisorientation",
                "spreadsheetapi/apichart/sethoraxisticklabelposition",
                "spreadsheetapi/apichart/sethoraxistitle",
                "spreadsheetapi/apichart/setlegendpos",
                "spreadsheetapi/apichart/setshowdatalabels",
                "spreadsheetapi/apichart/settitle",
                "spreadsheetapi/apichart/setveraxisorientation",
                "spreadsheetapi/apichart/setveraxistitle",
                "spreadsheetapi/apichart/setvertaxisticklabelposition",
                "spreadsheetapi/apicolor",
                "spreadsheetapi/apicolor/getclasstype",
                "spreadsheetapi/apidocumentcontent",
                "spreadsheetapi/apidocumentcontent/addelement",
                "spreadsheetapi/apidocumentcontent/getclasstype",
                "spreadsheetapi/apidocumentcontent/getelement",
                "spreadsheetapi/apidocumentcontent/getelementscount",
                "spreadsheetapi/apidocumentcontent/push",
                "spreadsheetapi/apidocumentcontent/removeallelements",
                "spreadsheetapi/apidocumentcontent/removeelement",
                "spreadsheetapi/apidrawing",
                "spreadsheetapi/apidrawing/getclasstype",
                "spreadsheetapi/apidrawing/setposition",
                "spreadsheetapi/apidrawing/setsize",
                "spreadsheetapi/apifill",
                "spreadsheetapi/apifill/getclasstype",
                "spreadsheetapi/apigradientstop",
                "spreadsheetapi/apigradientstop/getclasstype",
                "spreadsheetapi/apiimage",
                "spreadsheetapi/apiimage/getclasstype",
                "spreadsheetapi/apiparagraph",
                "spreadsheetapi/apiparagraph/addelement",
                "spreadsheetapi/apiparagraph/addlinebreak",
                "spreadsheetapi/apiparagraph/addtabstop",
                "spreadsheetapi/apiparagraph/addtext",
                "spreadsheetapi/apiparagraph/getclasstype",
                "spreadsheetapi/apiparagraph/getelement",
                "spreadsheetapi/apiparagraph/getelementscount",
                "spreadsheetapi/apiparagraph/getparapr",
                "spreadsheetapi/apiparagraph/removeallelements",
                "spreadsheetapi/apiparagraph/removeelement",
                "spreadsheetapi/apiparagraph/setbullet",
                "spreadsheetapi/apiparagraph/setindfirstline",
                "spreadsheetapi/apiparagraph/setindleft",
                "spreadsheetapi/apiparagraph/setindright",
                "spreadsheetapi/apiparagraph/setjc",
                "spreadsheetapi/apiparagraph/setspacingafter",
                "spreadsheetapi/apiparagraph/setspacingbefore",
                "spreadsheetapi/apiparagraph/setspacingline",
                "spreadsheetapi/apiparagraph/settabs",
                "spreadsheetapi/apiparapr",
                "spreadsheetapi/apiparapr/getclasstype",
                "spreadsheetapi/apiparapr/setbullet",
                "spreadsheetapi/apiparapr/setindfirstline",
                "spreadsheetapi/apiparapr/setindleft",
                "spreadsheetapi/apiparapr/setindright",
                "spreadsheetapi/apiparapr/setjc",
                "spreadsheetapi/apiparapr/setspacingafter",
                "spreadsheetapi/apiparapr/setspacingbefore",
                "spreadsheetapi/apiparapr/setspacingline",
                "spreadsheetapi/apiparapr/settabs",
                "spreadsheetapi/apipresetcolor",
                "spreadsheetapi/apipresetcolor/getclasstype",
                "spreadsheetapi/apirange",
                "spreadsheetapi/apirange/getcol",
                "spreadsheetapi/apirange/getrow",
                "spreadsheetapi/apirange/merge",
                "spreadsheetapi/apirange/setalignhorizontal",
                "spreadsheetapi/apirange/setalignvertical",
                "spreadsheetapi/apirange/setbold",
                "spreadsheetapi/apirange/setborders",
                "spreadsheetapi/apirange/setfillcolor",
                "spreadsheetapi/apirange/setfontcolor",
                "spreadsheetapi/apirange/setfontname",
                "spreadsheetapi/apirange/setfontsize",
                "spreadsheetapi/apirange/setitalic",
                "spreadsheetapi/apirange/setnumberformat",
                "spreadsheetapi/apirange/setstrikeout",
                "spreadsheetapi/apirange/setunderline",
                "spreadsheetapi/apirange/setvalue",
                "spreadsheetapi/apirange/setwrap",
                "spreadsheetapi/apirange/unmerge",
                "spreadsheetapi/apirgbcolor",
                "spreadsheetapi/apirgbcolor/getclasstype",
                "spreadsheetapi/apirun",
                "spreadsheetapi/apirun/addlinebreak",
                "spreadsheetapi/apirun/addtabstop",
                "spreadsheetapi/apirun/addtext",
                "spreadsheetapi/apirun/clearcontent",
                "spreadsheetapi/apirun/getclasstype",
                "spreadsheetapi/apirun/gettextpr",
                "spreadsheetapi/apirun/setbold",
                "spreadsheetapi/apirun/setcaps",
                "spreadsheetapi/apirun/setdoublestrikeout",
                "spreadsheetapi/apirun/setfill",
                "spreadsheetapi/apirun/setfontfamily",
                "spreadsheetapi/apirun/setfontsize",
                "spreadsheetapi/apirun/setitalic",
                "spreadsheetapi/apirun/setsmallcaps",
                "spreadsheetapi/apirun/setspacing",
                "spreadsheetapi/apirun/setstrikeout",
                "spreadsheetapi/apirun/setunderline",
                "spreadsheetapi/apirun/setvertalign",
                "spreadsheetapi/apischemecolor",
                "spreadsheetapi/apischemecolor/getclasstype",
                "spreadsheetapi/apishape",
                "spreadsheetapi/apishape/getclasstype",
                "spreadsheetapi/apishape/getdoccontent",
                "spreadsheetapi/apishape/setverticaltextalign",
                "spreadsheetapi/apistroke",
                "spreadsheetapi/apistroke/getclasstype",
                "spreadsheetapi/apitextpr",
                "spreadsheetapi/apitextpr/getclasstype",
                "spreadsheetapi/apitextpr/setbold",
                "spreadsheetapi/apitextpr/setcaps",
                "spreadsheetapi/apitextpr/setdoublestrikeout",
                "spreadsheetapi/apitextpr/setfill",
                "spreadsheetapi/apitextpr/setfontfamily",
                "spreadsheetapi/apitextpr/setfontsize",
                "spreadsheetapi/apitextpr/setitalic",
                "spreadsheetapi/apitextpr/setsmallcaps",
                "spreadsheetapi/apitextpr/setspacing",
                "spreadsheetapi/apitextpr/setstrikeout",
                "spreadsheetapi/apitextpr/setunderline",
                "spreadsheetapi/apitextpr/setvertalign",
                "spreadsheetapi/apiunicolor",
                "spreadsheetapi/apiunicolor/getclasstype",
                "spreadsheetapi/apiworksheet",
                "spreadsheetapi/apiworksheet/addchart",
                "spreadsheetapi/apiworksheet/addimage",
                "spreadsheetapi/apiworksheet/addshape",
                "spreadsheetapi/apiworksheet/formatastable",
                "spreadsheetapi/apiworksheet/getrange",
                "spreadsheetapi/apiworksheet/getrangebynumber",
                "spreadsheetapi/apiworksheet/setcolumnwidth",
                "spreadsheetapi/apiworksheet/setdisplaygridlines",
                "spreadsheetapi/apiworksheet/setdisplayheadings",
                "spreadsheetapi/apiworksheet/setname",
                "presentationapi",
                "presentationapi/api",
                "presentationapi/api/createblipfill",
                "presentationapi/api/createbullet",
                "presentationapi/api/createchart",
                "presentationapi/api/creategradientstop",
                "presentationapi/api/createimage",
                "presentationapi/api/createlineargradientfill",
                "presentationapi/api/createnofill",
                "presentationapi/api/createnumbering",
                "presentationapi/api/createparagraph",
                "presentationapi/api/createpatternfill",
                "presentationapi/api/createpresetcolor",
                "presentationapi/api/createradialgradientfill",
                "presentationapi/api/creatergbcolor",
                "presentationapi/api/createrun",
                "presentationapi/api/createschemecolor",
                "presentationapi/api/createshape",
                "presentationapi/api/createslide",
                "presentationapi/api/createsolidfill",
                "presentationapi/api/createstroke",
                "presentationapi/api/getpresentation",
                "presentationapi/apibullet",
                "presentationapi/apibullet/getclasstype",
                "presentationapi/apichart",
                "presentationapi/apichart/getclasstype",
                "presentationapi/apichart/sethoraxistitle",
                "presentationapi/apichart/setlegendpos",
                "presentationapi/apichart/setshowdatalabels",
                "presentationapi/apichart/settitle",
                "presentationapi/apichart/setveraxistitle",
                "presentationapi/apidocumentcontent",
                "presentationapi/apidocumentcontent/addelement",
                "presentationapi/apidocumentcontent/getclasstype",
                "presentationapi/apidocumentcontent/getelement",
                "presentationapi/apidocumentcontent/getelementscount",
                "presentationapi/apidocumentcontent/push",
                "presentationapi/apidocumentcontent/removeallelements",
                "presentationapi/apidocumentcontent/removeelement",
                "presentationapi/apidrawing",
                "presentationapi/apidrawing/getclasstype",
                "presentationapi/apidrawing/setposition",
                "presentationapi/apidrawing/setsize",
                "presentationapi/apifill",
                "presentationapi/apifill/getclasstype",
                "presentationapi/apigradientstop",
                "presentationapi/apigradientstop/getclasstype",
                "presentationapi/apiimage",
                "presentationapi/apiimage/getclasstype",
                "presentationapi/apiparagraph",
                "presentationapi/apiparagraph/addelement",
                "presentationapi/apiparagraph/addlinebreak",
                "presentationapi/apiparagraph/addtabstop",
                "presentationapi/apiparagraph/addtext",
                "presentationapi/apiparagraph/getclasstype",
                "presentationapi/apiparagraph/getelement",
                "presentationapi/apiparagraph/getelementscount",
                "presentationapi/apiparagraph/getparapr",
                "presentationapi/apiparagraph/removeallelements",
                "presentationapi/apiparagraph/removeelement",
                "presentationapi/apiparagraph/setbullet",
                "presentationapi/apiparagraph/setindfirstline",
                "presentationapi/apiparagraph/setindleft",
                "presentationapi/apiparagraph/setindright",
                "presentationapi/apiparagraph/setjc",
                "presentationapi/apiparagraph/setspacingafter",
                "presentationapi/apiparagraph/setspacingbefore",
                "presentationapi/apiparagraph/setspacingline",
                "presentationapi/apiparagraph/settabs",
                "presentationapi/apiparapr",
                "presentationapi/apiparapr/getclasstype",
                "presentationapi/apiparapr/setbullet",
                "presentationapi/apiparapr/setindfirstline",
                "presentationapi/apiparapr/setindleft",
                "presentationapi/apiparapr/setindright",
                "presentationapi/apiparapr/setjc",
                "presentationapi/apiparapr/setspacingafter",
                "presentationapi/apiparapr/setspacingbefore",
                "presentationapi/apiparapr/setspacingline",
                "presentationapi/apiparapr/settabs",
                "presentationapi/apipresentation",
                "presentationapi/apipresentation/addslide",
                "presentationapi/apipresentation/getclasstype",
                "presentationapi/apipresentation/getcurslideindex",
                "presentationapi/apipresentation/getcurrentslide",
                "presentationapi/apipresentation/getslidebyindex",
                "presentationapi/apipresentation/setsizes",
                "presentationapi/apipresetcolor",
                "presentationapi/apipresetcolor/getclasstype",
                "presentationapi/apirgbcolor",
                "presentationapi/apirgbcolor/getclasstype",
                "presentationapi/apirun",
                "presentationapi/apirun/addlinebreak",
                "presentationapi/apirun/addtabstop",
                "presentationapi/apirun/addtext",
                "presentationapi/apirun/clearcontent",
                "presentationapi/apirun/getclasstype",
                "presentationapi/apirun/gettextpr",
                "presentationapi/apirun/setbold",
                "presentationapi/apirun/setcaps",
                "presentationapi/apirun/setdoublestrikeout",
                "presentationapi/apirun/setfill",
                "presentationapi/apirun/setfontfamily",
                "presentationapi/apirun/setfontsize",
                "presentationapi/apirun/setitalic",
                "presentationapi/apirun/setsmallcaps",
                "presentationapi/apirun/setspacing",
                "presentationapi/apirun/setstrikeout",
                "presentationapi/apirun/setunderline",
                "presentationapi/apirun/setvertalign",
                "presentationapi/apischemecolor",
                "presentationapi/apischemecolor/getclasstype",
                "presentationapi/apishape",
                "presentationapi/apishape/getclasstype",
                "presentationapi/apishape/getdoccontent",
                "presentationapi/apishape/setverticaltextalign",
                "presentationapi/apislide",
                "presentationapi/apislide/addobject",
                "presentationapi/apislide/getclasstype",
                "presentationapi/apislide/getheight",
                "presentationapi/apislide/getwidth",
                "presentationapi/apislide/removeallobjects",
                "presentationapi/apislide/setbackground",
                "presentationapi/apistroke",
                "presentationapi/apistroke/getclasstype",
                "presentationapi/apitextpr",
                "presentationapi/apitextpr/getclasstype",
                "presentationapi/apitextpr/setbold",
                "presentationapi/apitextpr/setcaps",
                "presentationapi/apitextpr/setdoublestrikeout",
                "presentationapi/apitextpr/setfill",
                "presentationapi/apitextpr/setfontfamily",
                "presentationapi/apitextpr/setfontsize",
                "presentationapi/apitextpr/setitalic",
                "presentationapi/apitextpr/setsmallcaps",
                "presentationapi/apitextpr/setspacing",
                "presentationapi/apitextpr/setstrikeout",
                "presentationapi/apitextpr/setunderline",
                "presentationapi/apitextpr/setvertalign",
                "presentationapi/apiunicolor",
                "presentationapi/apiunicolor/getclasstype",
                "global",
                "classlist",
            };

        #endregion

        public ActionResult Navigation()
        {
            return View();
        }

        public ActionResult Search(string query)
        {
            var result = new List<SearchResult>();

            foreach (var action in _actionMap)
            {
                var actionString = action.ToLower();
                var doc = new HtmlDocument();
                try
                {
                    var html = this.RenderView(actionString, new ViewDataDictionary());
                    doc.LoadHtml(html);
                }
                catch (Exception e)
                {
                    LogManager.GetLogger("ASC.Api").Error(e);
                }
                var content = doc.DocumentNode;
                if (content.SelectSingleNode("html") != null)
                {
                    content = content.SelectSingleNode("//div[contains(@class, 'layout-content')]");
                }

                if (!string.IsNullOrEmpty(query) && content != null && content.InnerText.ToLowerInvariant().Contains(query.ToLowerInvariant()))
                {
                    var headerNode = doc.DocumentNode.SelectSingleNode("//span[@class='hdr']");
                    var descrNode = doc.DocumentNode.SelectSingleNode("//p[@class='dscr']");
                    var header = headerNode != null ? headerNode.InnerText : string.Empty;
                    var descr = descrNode != null ? descrNode.InnerText : string.Empty;
                    result.Add(new SearchResult
                        {
                            Module = "docbuilder",
                            Name = actionString,
                            Resource = Highliter.HighliteString(header, query).ToHtmlString(),
                            Description = Highliter.HighliteString(descr, query).ToHtmlString(),
                            Url = Url.Action(actionString, "docbuilder")
                        });
                }
            }

            ViewData["query"] = query ?? string.Empty;
            ViewData["result"] = result;
            return View(new Dictionary<MsDocEntryPoint, Dictionary<MsDocEntryPointMethod, string>>());
        }

        public ActionResult Basic()
        {
            return View();
        }

        public ActionResult Gettingstarted()
        {
            return View();
        }

        public ActionResult Nodejsexample()
        {
            return View();
        }

        public ActionResult Rubyexample()
        {
            return View();
        }

        public ActionResult Phpexample()
        {
            return View();
        }

        public ActionResult Csharpexample()
        {
            return View();
        }

        public ActionResult Integratingdocumentbuilder()
        {
            var directoryInfo = new DirectoryInfo(Request.MapPath("~/app_data/docbuilder"));

            var examples = directoryInfo.GetFiles("*.zip", SearchOption.TopDirectoryOnly).Select(fileInfo => fileInfo.Name).ToList();

            return View(examples);
        }

        public ActionResult Integrationapi(string catchall)
        {
            if (!_actionMap.Contains("integrationapi/" + catchall, StringComparer.OrdinalIgnoreCase))
            {
                catchall = null;
            }
            return View("Integrationapi", (object)catchall);
        }

        public ActionResult Textdocumentapi(string catchall)
        {
            if (!_actionMap.Contains("textdocumentapi/" + catchall, StringComparer.OrdinalIgnoreCase))
            {
                catchall = null;
            }
            return View("Textdocumentapi", (object)catchall);
        }

        public ActionResult Spreadsheetapi(string catchall)
        {
            if (!_actionMap.Contains("spreadsheetapi/" + catchall, StringComparer.OrdinalIgnoreCase))
            {
                catchall = null;
            }
            return View("Spreadsheetapi", (object)catchall);
        }

        public ActionResult Presentationapi(string catchall)
        {
            if (!_actionMap.Contains("presentationapi/" + catchall, StringComparer.OrdinalIgnoreCase))
            {
                catchall = null;
            }
            return View("Presentationapi", (object)catchall);
        }

        public ActionResult Global()
        {
            return View();
        }

        public ActionResult Classlist()
        {
            return View();
        }

        public FileResult DownloadScript(string fileId)
        {
            var hash = new Guid(fileId);
            var fileName = string.Format("{0}.docbuilder", hash);
            var builderPath = Path.Combine(Path.GetTempPath(), fileName);

            var bytes = System.IO.File.ReadAllBytes(builderPath);
            return File(bytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

        [HttpPost]
        public ActionResult DocBuilderGenerate(string actionName, string builderScript)
        {
            try
            {
                builderScript = (builderScript ?? "").Trim();
                if (string.IsNullOrEmpty(builderScript))
                    throw new Exception("Empty Script");

                var fileUrl = new DocBuilderHelper(Url, Request).GenerateDocument(builderScript);
                return Redirect(fileUrl);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ASC.DocumentBuilder").Error(ex);
                return RedirectToAction(actionName, new { error = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DocBuilderCreate(string actionName, string name, string company, string title, string format)
        {
            try
            {
                name = (name ?? "").Trim();
                if (string.IsNullOrEmpty(name))
                    name = "John Smith";

                company = (company ?? "").Trim();
                if (string.IsNullOrEmpty(company))
                    company = "ONLYOFFICE";

                title = (title ?? "").Trim();
                if (string.IsNullOrEmpty(title))
                    title = "Commercial director";

                var fileUrl = new DocBuilderHelper(Url, Request).CreateDocument(name, company, title, format);
                return Redirect(fileUrl);
            }
            catch (Exception ex)
            {
                LogManager.GetLogger("ASC.DocumentBuilder").Error(ex);
                return RedirectToAction(actionName, new { error = ex.Message });
            }
        }
    }
}