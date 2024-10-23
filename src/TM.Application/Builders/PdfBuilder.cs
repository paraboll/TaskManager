using System;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace TM.Application.Builders
{
    internal class PdfBuilder
    {
        private PdfDocument _document;
        private XGraphics _gfx;

        private double _yPoint;
        private double _columnWidth;
        private string[] _headers;

        public PdfBuilder()
        {
            _document = new PdfDocument();

            var page = _document.AddPage();
            _gfx = XGraphics.FromPdfPage(page);

            _yPoint = 60;
            _columnWidth = page.Width / 5;
        }

        public void SetTitle(string title)
        {
            _document.Info.Title = title;
 
            XFont headerFont = new XFont("Verdana", 14);
            _gfx.DrawString(title, headerFont, XBrushes.Black, new XRect(0, 20, _document.Pages[0].Width, 40), XStringFormats.TopCenter);
        }

        public void SetHeaders(string[] headers)
        {
            _headers = headers;
            AddTableHeader();
        }

        public void AddRow(string[] values)
        {
            if (_yPoint > _gfx.PdfPage.Height - 40)
            {
                var page = _document.AddPage();
                _gfx = XGraphics.FromPdfPage(page);
                _yPoint = 60;

                AddTableHeader();
            }

            XFont font = new XFont("Verdana", 12);
            foreach (var value in values)
            {
                _gfx.DrawString(value, font, XBrushes.Black, new XRect(_columnWidth * Array.IndexOf(values, value), _yPoint, _columnWidth, 20), XStringFormats.TopLeft);
            }

            _yPoint += 20;
        }

        public void Save(string filePath)
        {
            _document.Save(filePath);
        }

        private void AddTableHeader()
        {
            if (_document == null) return;

            XFont font = new XFont("Verdana", 12);
            foreach (var header in _headers)
            {
                _gfx.DrawString(header, font, XBrushes.Black, new XRect(_columnWidth * Array.IndexOf(_headers, header), _yPoint, _columnWidth, 20), XStringFormats.TopLeft);
            }
            _yPoint += 20;
        }
    }
}
