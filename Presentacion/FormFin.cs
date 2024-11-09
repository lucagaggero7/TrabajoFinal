using iTextSharp.text.pdf;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using iTextSharp.text;
using Font = iTextSharp.text.Font;

namespace Presentacion
{
    public partial class FormFin : FrmBase
    {
        private DataTable productosTable;
        public FormFin(DataTable productos)
        {
            InitializeComponent();
            productosTable = productos; // Guardamos el DataTable
        }

        private void FormFin_Load(object sender, EventArgs e)
        {
            // Divide el texto para tomar el nombre
            string nombre = ((FormComprar)Owner).txtNombre.Text.Split(' ')[0];
            string apellido = ((FormComprar)Owner).txtNombre.Text.Split(' ')[1];
            // Muestra un mensaje de felicitaciones junto al nombre en el label
            lblFin.Text = $"¡¡Felicitaciones por tu compra {nombre}!! \nVuelve pronto...";

            // Generar el PDF al cargar el formulario
            GenerarPDF();
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Hide(); // Oculta el formulario actual
            Owner.Owner.Owner.Show(); // Muestra el formulario padre

            // Borra todos los productos del carrito ya finalizada la compra
            ((FormCarrito)Owner.Owner).carrito.Productos.Rows.Clear();
        }

        private void GenerarPDF()
        {
            // Define el nombre del archivo PDF
            string pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "ReporteProductos.pdf");

            // Crear el documento PDF
            using (FileStream fs = new FileStream(pdfPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                Document document = new Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);

                // Abrir el documento para escribir
                document.Open();

                // Agregar el logo (ajusta la ruta de acuerdo a tu archivo de imagen)
                string logoPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo.png");
                iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
                logo.ScaleToFit(144f, 72f); // Aumentar el tamaño del logo en un 20%
                logo.Alignment = Element.ALIGN_LEFT; // Alinear a la izquierda
                logo.SpacingBefore = 10f; // Espacio antes del logo
                logo.SpacingAfter = 10f; // Espacio después del logo
                document.Add(logo);

                // Agregar un cuadro de texto con bordes que diga "FACTURA DE COMPRA ELECTRÓNICA"
                PdfPTable headerTable = new PdfPTable(1);
                headerTable.SpacingBefore = 20f; // Aumentar espacio antes de la tabla
                headerTable.SpacingAfter = 10f; // Espacio después de la tabla
                PdfPCell headerCell = new PdfPCell(new Phrase("FACTURA DE COMPRA ELECTRÓNICA", new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, iTextSharp.text.Font.BOLD)))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    BorderWidth = 1,
                    BorderColor = BaseColor.BLACK,
                    Padding = 10
                };
                headerTable.AddCell(headerCell);
                document.Add(headerTable);

                // Obtener nombre y apellido
                string nombre = ((FormComprar)Owner).txtNombre.Text.Split(' ')[0];
                string apellido = ((FormComprar)Owner).txtNombre.Text.Split(' ')[1];

                // Agregar nombre y apellido en la parte superior derecha
                Paragraph nameParagraph = new Paragraph($"Cliente: {nombre} {apellido}", new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL))
                {
                    Alignment = Element.ALIGN_RIGHT // Alinear a la derecha
                };
                document.Add(nameParagraph);

                // Agregar un título
                Font titleFont = new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 24, iTextSharp.text.Font.BOLD);
                Paragraph title = new Paragraph("Productos", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                document.Add(new Paragraph("\n")); // Espacio en blanco

                // Crear tabla
                PdfPTable table = new PdfPTable(4); // 4 columnas para Nombre, Marca, Categoria y Precio
                table.AddCell("Nombre");
                table.AddCell("Marca");
                table.AddCell("Categoria");
                table.AddCell("Precio");

                decimal total = 0; // Variable para almacenar el total

                // Verificar si hay datos en la DataTable
                if (productosTable != null && productosTable.Rows.Count > 0)
                {
                    // Agregar los productos a la tabla
                    foreach (DataRow row in productosTable.Rows)
                    {
                        table.AddCell(row["Nombre"].ToString());
                        table.AddCell(row["Marca"].ToString());
                        table.AddCell(row["Categoria"].ToString());
                        decimal precio = Convert.ToDecimal(row["Precio"]);
                        table.AddCell(precio.ToString("C")); // Formato de moneda
                        total += precio; // Sumar al total
                    }
                    ///
                }
                else
                {
                    // Si no hay datos, agregar un mensaje
                    table.AddCell("No hay productos disponibles");
                    table.AddCell("");
                    table.AddCell("");
                    table.AddCell("");
                }

                document.Add(table);

                // Agregar el total debajo de la tabla
                Paragraph totalParagraph = new Paragraph($"Total: {total.ToString("C")}", new Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.BOLD))
                {
                    Alignment = Element.ALIGN_RIGHT // Alinear a la derecha
                };
                document.Add(totalParagraph);

                // Agregar logo2.png al final, aumentado en un 20%
                string logo2Path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "logo2.png");
                iTextSharp.text.Image logo2 = iTextSharp.text.Image.GetInstance(logo2Path);
                logo2.ScaleToFit(144f, 72f); // Aumentar el tamaño del segundo logo en un 20%
                logo2.Alignment = Element.ALIGN_RIGHT; // Alinear a la derecha
                logo2.SpacingBefore = 20f; // Aumentar el espacio antes del logo2
                logo2.SpacingAfter = 10f; // Espacio después del logo2
                document.Add(logo2);

                document.Close(); // Cerrar el documento
            }

            // Muestra el PDF
            System.Diagnostics.Process.Start(pdfPath); // Abre el PDF en el visor predeterminado
        }
    }
}
