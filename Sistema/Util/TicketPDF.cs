using System.Collections;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Font = iTextSharp.text.Font;
using Image = iTextSharp.text.Image;
using Rectangle = iTextSharp.text.Rectangle;

namespace Sistema.Util
{
    public class TicketPDF
    {
        public TicketPDF()
        {
            myDocument.AddAuthor("SistemaNET");
            myDocument.AddCreator("GUSTAVO QUEVEDO");
            myDocument.AddTitle("Ticket de Venta");
        }

        PdfWriter writer = null;
        PdfContentByte cb = null;
        ArrayList headerLines = new ArrayList();
        ArrayList subHeaderLines = new ArrayList();
        ArrayList items = new ArrayList();
        ArrayList totales = new ArrayList();
        ArrayList footerLines = new ArrayList();
        private string headerImage = "";
        private string footerQR = "";
        bool _DrawItemHeaders = true;
        int count = 0;
        string path = "";

        int maxChar = 64;
        int maxCharDescription = 30;

        int imageHeight = 0;
        int imageHeight2 = 0;

        static int fontSize = 8;

        static BaseFont bfCourier =
            BaseFont.CreateFont(BaseFont.COURIER, BaseFont.CP1252, false);

        static Font font = new
            Font(bf: bfCourier, fontSize, Font.NORMAL, color: BaseColor.BLACK);

        Document myDocument = new
            Document(pageSize: new Rectangle(360f, 500f), marginLeft: 25, marginRight: 25, marginTop: 20, marginBottom: 10);

        string line = "";
        #region Properties

        public String Path
        {
            get { return path; }
            set { path = value; }
        }

        public String HeaderImage
        {
            get { return headerImage; }
            set { if (headerImage != value) headerImage = value; }
        }
        public String FooterQR
        {
            get { return footerQR; }
            set { if (footerQR != value) footerQR = value; }
        }

        public int MaxChar
        {
            get { return maxChar; }
            set { if (value != maxChar) maxChar = value; }
        }

        public bool DrawItemHeaders
        {
            set { _DrawItemHeaders = value; }
        }

        public int MaxCharDescription
        {
            get { return maxCharDescription; }
            set { if (value != maxCharDescription) maxCharDescription = value; }
        }

        public int FontSize
        {
            get { return fontSize; }
            set { if (value != fontSize) fontSize = value; }
        }

        public Font FontName
        {
            get { return font; }
            set { if (value != font) font = value; }
        }

        #endregion

        public void AddHeaderLine(string line)
        {
            headerLines.Add(line);
        }

        public void AddSubHeaderLine(string line)
        {
            subHeaderLines.Add(line);
        }

        public void AddItem(string cantidad, string item, string price, string total)
        {
            TicketOrderItem newItem = new TicketOrderItem('?');
            items.Add(newItem.GenerateItem(cantidad, item, price, total));
        }

        public void AddTotal(string name, string price)
        {
            TicketOrderTotal newTotal = new TicketOrderTotal('?');
            totales.Add(newTotal.GenerateTotal(name, price));
        }

        public void AddFooterLine(string line)
        {
            footerLines.Add(line);
        }

        private string AlignRightText(int lenght)
        {
            string espacios = "";
            int spaces = maxChar - lenght;
            for (int x = 0; x < spaces; x++)
                espacios += " ";
            return espacios;
        }

        private string DottedLine()
        {
            string dotted = "";
            for (int x = 0; x < maxChar; x++)
                dotted += "-";
            return dotted;
        }

        public bool Print()
        {
            try
            {
                //aqui para generar el PDF
                writer = PdfWriter.GetInstance(myDocument,
                    new FileStream(path, FileMode.Create));

                myDocument.Open();
                cb = writer.DirectContent;
                cb.SetFontAndSize(font.BaseFont, fontSize);
                cb.BeginText();
                DrawImage();
                DrawHeader();
                DrawSubHeader();
                DrawItems();
                DrawTotales();
                DrawFooter();
                DrawQR();
                DrawFinal();
                cb.EndText();
                myDocument.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private float YPosition()
        {
            return (myDocument.PageSize.Height -
                ((myDocument.TopMargin + myDocument.BottomMargin) + (count * font.CalculatedSize + imageHeight + imageHeight2)));
        }

        private void DrawImage()
        {
            try
            {
                if ((headerImage != null) && (headerImage != ""))
                {
                    if (File.Exists(headerImage))
                    {
                        Image logo = Image.GetInstance(headerImage);
                        double height = ((double)logo.Height / 70) * 5;
                        imageHeight = (int)Math.Round(height) + 3;
                        logo.SetAbsolutePosition((myDocument.PageSize.Width / 2) - (imageHeight/2), myDocument.PageSize.Height - imageHeight);
                        logo.ScaleToFit(imageHeight, imageHeight);
                        myDocument.Add(logo);
                    }
                }
            }
            catch (Exception ex) { throw (ex); }
        }

        private void DrawHeader()
        {
            try
            {
                foreach (string header in headerLines)
                {
                    if (header.Length > maxChar)
                    {
                        int currentChar = 0;
                        int headerLenght = header.Length;
                        while (headerLenght > maxChar)
                        {
                            line = header;
                            cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line.Substring(currentChar, maxChar), x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                            count++;
                            currentChar += maxChar;
                            headerLenght -= maxChar;
                        }
                        line = header;
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line.Substring(currentChar, line.Length - currentChar), x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                        count++;
                    }
                    else
                    {
                        line = header;
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line, x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                        count++;
                    }
                }
                DrawEspacio();
            }
            catch (Exception ex) { throw (ex); }
        }

        private void DrawSubHeader()
        {
            try
            {
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                DrawEspacio();
                foreach (string subHeader in subHeaderLines)
                {
                    if (subHeader.Length > maxChar)
                    {
                        int currentChar = 0;
                        int subHeaderLenght = subHeader.Length;
                        while (subHeaderLenght > maxChar)
                        {
                            line = subHeader;
                            cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line.Substring(currentChar, maxChar), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                            count++;
                            currentChar += maxChar;
                            subHeaderLenght -= maxChar;
                        }
                        line = subHeader;
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line.Substring(currentChar, line.Length - currentChar), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                        count++;
                    }
                    else
                    {
                        line = subHeader;
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line, x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                        count++;
                    }
                }
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                DrawEspacio();
                DrawEspacio();
            }
            catch (Exception ex) { throw (ex); }
        }

        private void DrawItems()
        {
            TicketOrderItem ordIt = new TicketOrderItem('?');
            if (_DrawItemHeaders)
            {
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                DrawEspacio();
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: "CANT   DESCRIPCION                    P. UNIT        SUBTOTAL", x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                DrawEspacio();
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                DrawEspacio();
            }
            count++;
            foreach (string item in items)
            {
                string cantidad = ordIt.GetItemCantidad(item);
                string precio = ordIt.GetItemPrice(item);
                string total = ordIt.GetItemTotal(item);

                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: cantidad, x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: $"{AlignRightText((precio.Length + total.Length + 15))} {precio}", x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: $"{AlignRightText((total.Length + 5))}  {total}", x: myDocument.LeftMargin, y: YPosition(), rotation: 0);

                string name = ordIt.GetItemName(item);
                if (name.Length > maxCharDescription)
                {
                    int currentChar = 0;
                    int itemLenght = name.Length;
                    while (itemLenght > maxCharDescription)
                    {
                        line = ordIt.GetItemName(item);
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: "       " + line.Substring(currentChar, maxCharDescription), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                        count++;
                        currentChar += maxCharDescription;
                        itemLenght -= maxCharDescription;
                    }
                    line = ordIt.GetItemName(item);
                    cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: "       " + line.Substring(currentChar, line.Length - currentChar), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                    count++;
                }
                else
                {
                    line = ordIt.GetItemName(item);
                    cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: "       " + line, x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                    count++;
                }
            }
            DrawEspacio();
        }

        private void DrawTotales()
        {
            TicketOrderTotal ordTot = new TicketOrderTotal('?');

            cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
            DrawEspacio();
            foreach (string total in totales)
            {
                string totalCantidad = ordTot.GetTotalCantidad(total);
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: $"{AlignRightText(totalCantidad.Length)}{totalCantidad}", x: myDocument.LeftMargin, y: YPosition(), rotation: 0);

                string totalNombre = ordTot.GetTotalName(total);
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: totalNombre, x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                count++;
            }
            cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: DottedLine(), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
            DrawEspacio();
            DrawEspacio();
        }

        private void DrawFooter()
        {
            foreach (string footer in footerLines)
            {
                if (footer.Length > maxChar)
                {
                    int currentChar = 0;
                    int footerLenght = footer.Length;
                    while (footerLenght > maxChar)
                    {
                        line = footer;
                        cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line.Substring(currentChar, maxChar), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                        count++;
                        currentChar += maxChar;
                        footerLenght -= maxChar;
                    }
                    line = footer;
                    cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line.Substring(currentChar, line.Length - currentChar), x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                    count++;
                }
                else
                {
                    line = footer;
                    cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_LEFT, text: line, x: myDocument.LeftMargin, y: YPosition(), rotation: 0);
                    count++;
                }
            }
            DrawEspacio();
        }

        private void DrawQR()
        {
            try
            {
                if ((footerQR != null) && (footerQR != ""))
                {
                    if (File.Exists(footerQR))
                    {
                        Image logo = Image.GetInstance(footerQR);
                        double height = ((double)logo.Height / 70) * 15;
                        imageHeight2 = (int)Math.Round(height) + 3;
                        logo.SetAbsolutePosition((myDocument.PageSize.Width / 2) - (imageHeight / 2), YPosition());
                        logo.ScaleToFit(imageHeight2, imageHeight2);
                        myDocument.Add(logo);

                        DrawEspacio();
                        DrawEspacio();
                    }
                }
            }
            catch (Exception ex) { throw (ex); }
        }

        private void DrawFinal()
        {
            string text = "ESTA FACTURA CONTRIBUYE AL DESARROLLO DEL PAÍS EL USO ILÍCITO DE ESTA SERA SANCIONADO DE ACUERDO A LA LEY";
            if (text.Length > maxChar)
            {
                int currentChar = 0;
                int footerLenght = text.Length;
                while (footerLenght > maxChar)
                {
                    line = text;
                    cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line.Substring(currentChar, maxChar), x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                    count++;
                    currentChar += maxChar;
                    footerLenght -= maxChar;
                }
                line = text;
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line.Substring(currentChar, line.Length - currentChar), x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                count++;
            }
            else
            {
                line = text;
                cb.ShowTextAligned(alignment: PdfContentByte.ALIGN_CENTER, text: line, x: myDocument.PageSize.Width / 2, y: YPosition(), rotation: 0);
                count++;
            }
        }

        private void DrawEspacio()
        {
            line = "";
            cb.SetTextMatrix(0, YPosition());
            cb.SetFontAndSize(font.BaseFont, fontSize);
            cb.ShowText(line);
            count++;
        }
    }

    public class TicketOrderItem
    {
        char[] delimitador = new char[] { '?' };
        public TicketOrderItem(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetItemCantidad(string TicketOrderItem)
        {
            string[] delimitado = TicketOrderItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetItemName(string TicketOrderItem)
        {
            string[] delimitado = TicketOrderItem.Split(delimitador);
            return delimitado[1];
        }

        public string GetItemPrice(string TicketOrderItem)
        {
            string[] delimitado = TicketOrderItem.Split(delimitador);
            return delimitado[2];
        }

        public string GetItemTotal(string TicketOrderItem)
        {
            string[] delimitado = TicketOrderItem.Split(delimitador);
            return delimitado[3];
        }

        public string GenerateItem(string cantidad,
            string itemName, string price, string total)
        {
            return cantidad + delimitador[0] +
                itemName + delimitador[0] + price + delimitador[0] + total;
        }
    }

    public class TicketOrderTotal
    {
        char[] delimitador = new char[] { '?' };
        public TicketOrderTotal(char delimit)
        {
            delimitador = new char[] { delimit };
        }

        public string GetTotalName(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return delimitado[0];
        }

        public string GetTotalCantidad(string totalItem)
        {
            string[] delimitado = totalItem.Split(delimitador);
            return $"Q {delimitado[1]}";
        }

        public string GenerateTotal(string totalName,
            string price)
        {
            return totalName + delimitador[0] + price;
        }
    }
}
