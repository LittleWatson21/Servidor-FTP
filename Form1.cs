using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;

namespace ftp_add
{
    public partial class Form1 : Form
    {
       private void subirArchivos(string direccionFTP, string rutaArchivo, string usuario, string contrasena)
        {
            //Creamos la solicitud FTP
            FtpWebRequest request = (FtpWebRequest)FtpWebRequest.Create(direccionFTP + "/" + Path.GetFileName(rutaArchivo));

            request.Method = WebRequestMethods.Ftp.UploadFile;
            request.Credentials = new NetworkCredential(usuario, contrasena);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;


            //Cargando el archivo.
            FileStream stream = File.OpenRead(rutaArchivo);
            byte[] buffer = new byte[stream.Length];

            stream.Read(buffer, 0, buffer.Length);
            stream.Close();

            //Subiendo el archivo.
            Stream reqStream = request.GetRequestStream();
            reqStream.Write(buffer, 0, buffer.Length);
            reqStream.Close();

            MessageBox.Show("Subido exitosamente.");


        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonftp_Click(object sender, EventArgs e)
        {
            buttonftp.Enabled = false;
            Application.DoEvents();

            subirArchivos(txtdireccionFtp.Text, txtRutaArchivo.Text, txtUsuario.Text, txtContrasena.Text);
            buttonftp.Enabled = true;

        }

        private void txtdireccionFtp_TextChanged(object sender, EventArgs e)
        {
            if(!txtdireccionFtp.Text.StartsWith("ftp://"))
                txtdireccionFtp.Text = "ftp://" + txtdireccionFtp.Text;
            
        }

        private void btnRutaArchivo_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                txtRutaArchivo.Text = openFileDialog1.FileName;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
