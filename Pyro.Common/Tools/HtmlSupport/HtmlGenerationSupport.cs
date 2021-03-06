﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Pyro.Common.Interfaces.Tools.HtmlSupport
{
  public class HtmlGenerationSupport : IHtmlGenerationSupport
  {
    private ICollection<SectionBase> _SectionList;

    public HtmlGenerationSupport()
    {
      _SectionList = new List<SectionBase>();
    }

    public void NewValuePairList(string Desciption, string Value, string Heading = null, int HeaderStrength = 1)
    {
      ValuePair.NewValuePairList(_SectionList, Desciption, Value, Heading, HeaderStrength);
    }

    public void AppendValuePairList(string Desciption, string Value)
    {
      ValuePair.AppendValuePairList(_SectionList, Desciption, Value);
    }

    public void NewParagraph(string Value, string Heading = null, int HeaderStrength = 1)
    {
      Paragraph.NewParagraph(_SectionList, Value, Heading, HeaderStrength);
    }
    public void AppendParagraph(string Value)
    {
      Paragraph.AppendParagraph(_SectionList, Value);
    }

    public void NewTable(string Heading = null, int HeaderStrength = 1, bool IsHeaderRow = false, params string[] Values)
    {
      Table.NewTable(_SectionList, Heading, HeaderStrength, IsHeaderRow, Values);
    }
    public void NewRow(params string[] Values)
    {
      Table.NewRow(_SectionList, Values);
    }

    public string Generate()
    {
      if (_SectionList.Count > 0)
        _SectionList.Last<SectionBase>().End();

      var RootDiv = new XElement(SectionBase.div);
      foreach (var item in _SectionList)
      {
        if (item != null)
          RootDiv.Add(item.Generate());
      }
      _SectionList.Clear();
      return RootDiv.ToString();
    }

    private class ValuePair : SectionBase
    {
      private XElement _Result;
      public XElement Heading { get; set; }
      public ICollection<XElement> ElementList { get; set; }
      public ValuePair()
      {
        this.ElementList = new List<XElement>();
      }

      public static void NewValuePairList(ICollection<SectionBase> SectionBaseList, string Desciption, string Value, string Heading, int HeaderStrength = 1)
      {
        XElement Header = null;
        if (!string.IsNullOrWhiteSpace(Heading))
        {
          Header = new XElement(GetHeaderStrength(HeaderStrength));
          Header.Value = Heading;
        }

        XElement TableDataDesc = null;
        if (!string.IsNullOrWhiteSpace(Desciption))
        {
          XElement SpanDesc = new XElement(span);
          XElement Bold = new XElement(b);
          SpanDesc.Add(Bold);

          Bold.Value = string.Format("{0}: ", Desciption);
          TableDataDesc = new XElement(td);
          TableDataDesc.Add(SpanDesc);
        }

        XElement TableDataValue = null;
        if (!string.IsNullOrWhiteSpace(Value))
        {
          XElement SpanValue = new XElement(span);
          SpanValue.Value = Value;

          TableDataValue = new XElement(td);
          TableDataValue.Add(SpanValue);
        }

        XElement TableRow = null;
        if (TableDataDesc != null)
        {
          if (TableRow == null)
            TableRow = new XElement(tr);
          TableRow.Add(TableDataDesc);
        }
        if (TableDataValue != null)
        {
          if (TableRow == null)
            TableRow = new XElement(tr);
          TableRow.Add(TableDataValue);
        }

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          SectionBase.End();
        }

        ValuePair oValuePair = new ValuePair();
        if (Header != null)
          oValuePair.Heading = Header;

        if (TableRow != null)
          oValuePair.ElementList.Add(TableRow);

        if (oValuePair != null)
          SectionBaseList.Add(oValuePair);
      }

      public static void AppendValuePairList(ICollection<SectionBase> SectionBaseList, string Desciption, string Value)
      {
        XElement SpanDesc = new XElement(span);
        XElement Bold = new XElement(b);
        SpanDesc.Add(Bold);
        Bold.Value = string.Format("{0}: ", Desciption);
        XElement TableDataDesc = new XElement(td);
        TableDataDesc.Add(SpanDesc);

        XElement SpanValue = new XElement(span);
        SpanValue.Value = Value;

        XElement TableDataValue = new XElement(td);
        TableDataValue.Add(SpanValue);

        XElement TableRow = new XElement(tr);
        TableRow.Add(TableDataDesc);
        TableRow.Add(TableDataValue);

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          if (SectionBase is ValuePair oValuePair1)
          {
            oValuePair1.ElementList.Add(TableRow);
          }
          else
          {
            SectionBase.End();
            ValuePair oValuePair = new ValuePair();
            oValuePair.ElementList.Add(TableRow);
            SectionBaseList.Add(oValuePair);
          }
        }
        else
        {
          ValuePair oValuePair = new ValuePair();
          oValuePair.ElementList.Add(TableRow);
          SectionBaseList.Add(oValuePair);
        }
      }

      public override void End()
      {
        XElement Table = new XElement(table);
        XElement Div = new XElement(div);
        if (this.Heading != null)
          Div.Add(this.Heading);
        Div.Add(Table);
        foreach (XElement line in ElementList)
        {
          Table.Add(line);
        }
        _Result = Div;
      }

      public override XElement Generate()
      {
        return _Result;
      }
    }

    private class Paragraph : SectionBase
    {
      public XElement _result { get; set; }

      public XElement _Header { get; set; }
      private ICollection<XElement> _ParagraphCollction;

      public Paragraph()
      {
        this._ParagraphCollction = new List<XElement>();
      }

      public static void NewParagraph(ICollection<SectionBase> SectionBaseList, string Value, string Heading = null, int HeaderStrength = 1)
      {
        XElement Header = null;
        if (!string.IsNullOrWhiteSpace(Heading))
        {
          Header = new XElement(GetHeaderStrength(HeaderStrength), new XAttribute("style", "color: black;"));
          Header.Value = Heading;
        }

        XElement Para = new XElement(p);
        Para.Value = Value;

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          SectionBase.End();
        }
        Paragraph oParagraph = new Paragraph();
        if (Header != null)
          oParagraph._Header = Header;
        oParagraph._ParagraphCollction.Add(Para);
        SectionBaseList.Add(oParagraph);
      }

      public static void AppendParagraph(ICollection<SectionBase> SectionBaseList, string Value)
      {
        XElement Para = new XElement(p);
        Para.Value = Value;

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          if (SectionBase is Paragraph oParagraph)
          {
            oParagraph._ParagraphCollction.Add(Para);
          }
          else
          {
            SectionBase.End();
            Paragraph oParagraph2 = new Paragraph();
            oParagraph2._Header = null;
            oParagraph2._ParagraphCollction.Add(Para);
            SectionBaseList.Add(oParagraph2);
          }
        }
        else
        {
          Paragraph oParagraph = new Paragraph();
          oParagraph._Header = null;
          oParagraph._ParagraphCollction.Add(Para);
          SectionBaseList.Add(oParagraph);
        }
      }

      public override void End()
      {
        XElement Div = new XElement(div);
        Div.Add(_Header);
        foreach (var Para in _ParagraphCollction)
          Div.Add(Para);
        _result = Div;
      }

      public override XElement Generate()
      {
        return _result;
      }
    }

    private class Table : SectionBase
    {
      public XElement _result { get; set; }

      public XElement _Header { get; set; }
      private ICollection<XElement> _RowCollction;

      public Table()
      {
        this._RowCollction = new List<XElement>();
      }

      public static void NewTable(ICollection<SectionBase> SectionBaseList, string Heading = null, int HeaderStrength = 1, bool IsHeaderRow = false, params string[] Values)
      {
        XElement Header = null;
        if (!string.IsNullOrWhiteSpace(Heading))
        {
          Header = new XElement(GetHeaderStrength(HeaderStrength));
          Header.Value = Heading;
        }

        XElement Row = new XElement(tr);
        foreach (string Value in Values)
        {
          XElement Data = null;
          if (IsHeaderRow)
            Data = new XElement(th);
          else
            Data = new XElement(td);
          Data.Value = Value;
          Row.Add(Data);
        }

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          SectionBase.End();
        }
        Table oTable = new Table();
        if (Header != null)
          oTable._Header = Header;
        oTable._RowCollction.Add(Row);
        SectionBaseList.Add(oTable);
      }

      public static void NewRow(ICollection<SectionBase> SectionBaseList, params string[] Values)
      {

        XElement Row = new XElement(tr);
        foreach (string Value in Values)
        {
          XElement Data = new XElement(td);
          Data.Value = Value;
          Row.Add(Data);
        }

        SectionBase SectionBase = SectionBaseList.LastOrDefault();
        if (SectionBase != null)
        {
          if (SectionBase is Table)
          {
            Table oTable = SectionBase as Table;
            oTable._RowCollction.Add(Row);
          }
          else
          {
            SectionBase.End();
            Table oTable = new Table();
            oTable._Header = null;
            oTable._RowCollction.Add(Row);
            SectionBaseList.Add(oTable);
          }
        }
        else
        {
          Table oTable = new Table();
          oTable._Header = null;
          oTable._RowCollction.Add(Row);
          SectionBaseList.Add(oTable);
        }
      }

      public override void End()
      {
        XElement Div = new XElement(div);
        //border: 1px solid black;
        //border - collapse: collapse;
        XElement Table = new XElement(table);
        Div.Add(_Header);
        Div.Add(Table);
        foreach (var Row in _RowCollction)
          Table.Add(Row);
        _result = Div;
      }

      public override XElement Generate()
      {
        return _result;
      }
    }

    protected abstract class SectionBase
    {
      protected static XNamespace ns = Hl7.Fhir.Utility.XmlNs.XHTML;
      public static XName div = ns + "div";
      protected static XName span = ns + "span";
      protected static XName br = ns + "br";
      protected static XName p = ns + "p";
      protected static XName b = ns + "b";

      protected static XName ul = ns + "ul";
      protected static XName li = ns + "li";

      protected static XName table = ns + "table";
      protected static XName tr = ns + "tr";
      protected static XName th = ns + "th";
      protected static XName td = ns + "td";

      protected static XName h1 = ns + "h1";
      protected static XName h2 = ns + "h2";
      protected static XName h3 = ns + "h3";
      protected static XName h4 = ns + "h4";
      protected static XName h5 = ns + "h5";
      protected static XName h6 = ns + "h6";

      public abstract XElement Generate();
      protected static XName GetHeaderStrength(int number)
      {
        switch (number)
        {
          case 1:
            return h1;
          case 2:
            return h2;
          case 3:
            return h3;
          case 4:
            return h4;
          case 5:
            return h5;
          case 6:
            return h6;
          default:
            throw new ArgumentException("HTML Header can ony be 1-6 in strength, value given was: " + number.ToString());
        }

      }
      public abstract void End();
    }
  }
}



// FHIR Specified XHTML CSS Styles classes
//bold Bold { font-weight: bold }
//italics Italics Text	{ font-style: italic }
//underline Underlined Text	{ text-decoration: underline }
//strikethrough Strikethrough Text	{ text-decoration: line-through }
//left Left Aligned	{ text-align : left }
//right Right Aligned	{ text-align : right }
//center Center Aligned	{ text-align : center }
//justify Justified { text-align : justify }
//border-left Border on the left	{ border-left: 1px solid grey }
//border-right Border on the right	{ border-right: 1px solid grey }
//border-top Border on the top	{ border-top: 1px solid grey }
//border-bottom Border on the bottom	{ border-bottom: 1px solid grey }
//arabic List is ordered using Arabic numerals: 1, 2, 3	{ list-style-type: decimal }
//little-roman List is ordered using little Roman numerals: i, ii, iii	{ list-style-type: lower-roman }
//big-roman List is ordered using big Roman numerals: I, II, III	{ list-style-type: upper-roman }
//little-alpha List is ordered using little alpha characters: a, b, c	{ list-style-type: lower-alpha }
//big-alpha List is ordered using big alpha characters: A, B, C	{ list-style-type: upper-alpha }
//disc List bullets are simple solid discs	{ list-style-type: disc }
//circle List bullets are hollow discs { list-style-type : circle }
//square List bullets are solid squares { list-style-type: square }
//unlist List with no bullets	{ list-style-type: none }