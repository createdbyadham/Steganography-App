using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SteganographyApp
{
    public partial class MainForm : Form
    {
        private Bitmap originalImage;
        private Bitmap stegoImage;

        public MainForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            this.btnLoadImage = new System.Windows.Forms.Button();
            this.btnEncode = new System.Windows.Forms.Button();
            this.btnDecode = new System.Windows.Forms.Button();
            this.btnSaveImage = new System.Windows.Forms.Button();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.SuspendLayout();
            // 
            // btnLoadImage
            // 
            this.btnLoadImage.Location = new System.Drawing.Point(12, 12);
            this.btnLoadImage.Name = "btnLoadImage";
            this.btnLoadImage.Size = new System.Drawing.Size(100, 30);
            this.btnLoadImage.TabIndex = 0;
            this.btnLoadImage.Text = "Load Image";
            this.btnLoadImage.UseVisualStyleBackColor = true;
            this.btnLoadImage.Click += new System.EventHandler(this.btnLoadImage_Click);
            // 
            // btnEncode
            // 
            this.btnEncode.Location = new System.Drawing.Point(118, 12);
            this.btnEncode.Name = "btnEncode";
            this.btnEncode.Size = new System.Drawing.Size(100, 30);
            this.btnEncode.TabIndex = 1;
            this.btnEncode.Text = "Encode";
            this.btnEncode.UseVisualStyleBackColor = true;
            this.btnEncode.Click += new System.EventHandler(this.btnEncode_Click);
            // 
            // btnDecode
            // 
            this.btnDecode.Location = new System.Drawing.Point(224, 12);
            this.btnDecode.Name = "btnDecode";
            this.btnDecode.Size = new System.Drawing.Size(100, 30);
            this.btnDecode.TabIndex = 2;
            this.btnDecode.Text = "Decode";
            this.btnDecode.UseVisualStyleBackColor = true;
            this.btnDecode.Click += new System.EventHandler(this.btnDecode_Click);
            // 
            // btnSaveImage
            // 
            this.btnSaveImage.Location = new System.Drawing.Point(330, 12);
            this.btnSaveImage.Name = "btnSaveImage";
            this.btnSaveImage.Size = new System.Drawing.Size(100, 30);
            this.btnSaveImage.TabIndex = 3;
            this.btnSaveImage.Text = "Save Image";
            this.btnSaveImage.UseVisualStyleBackColor = true;
            this.btnSaveImage.Click += new System.EventHandler(this.btnSaveImage_Click);
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(12, 70);
            this.txtMessage.Multiline = true;
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(418, 100);
            this.txtMessage.TabIndex = 4;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(12, 54);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(107, 13);
            this.lblMessage.TabIndex = 5;
            this.lblMessage.Text = "Message to hide/view:";
            // 
            // pictureBox
            // 
            this.pictureBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox.Location = new System.Drawing.Point(12, 176);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(418, 300);
            this.pictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox.TabIndex = 6;
            this.pictureBox.TabStop = false;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Image Files|*.png;*.bmp;*.jpg;*.jpeg";
            this.openFileDialog.Title = "Select an Image";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "PNG Image|*.png|Bitmap Image|*.bmp";
            this.saveFileDialog.Title = "Save Steganography Image";
            this.saveFileDialog.DefaultExt = "png";
            // 
            // MainForm
            // 
            this.ClientSize = new System.Drawing.Size(442, 488);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.btnSaveImage);
            this.Controls.Add(this.btnDecode);
            this.Controls.Add(this.btnEncode);
            this.Controls.Add(this.btnLoadImage);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Steganography App";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private Button btnLoadImage;
        private Button btnEncode;
        private Button btnDecode;
        private Button btnSaveImage;
        private TextBox txtMessage;
        private Label lblMessage;
        private PictureBox pictureBox;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;

        private void btnLoadImage_Click(object sender, EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    originalImage = new Bitmap(openFileDialog.FileName);
                    pictureBox.Image = originalImage;
                    stegoImage = null;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = txtMessage.Text;
            if (string.IsNullOrEmpty(message))
            {
                MessageBox.Show("Please enter a message to hide.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                stegoImage = HideMessage(originalImage, message);
                pictureBox.Image = stegoImage;
                MessageBox.Show("Message hidden successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error encoding message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                MessageBox.Show("Please load an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Bitmap image = (Bitmap)pictureBox.Image;
                string message = ExtractMessage(image);
                txtMessage.Text = message;
                MessageBox.Show("Message extracted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error decoding message: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSaveImage_Click(object sender, EventArgs e)
        {
            if (stegoImage == null)
            {
                MessageBox.Show("Please encode a message first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                    if (extension == ".png")
                    {
                        stegoImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    else if (extension == ".bmp")
                    {
                        stegoImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Bmp);
                    }
                    else
                    {
                        stegoImage.Save(saveFileDialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
                    }
                    MessageBox.Show("Image saved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private Bitmap HideMessage(Bitmap image, string message)
        {
            // Create a copy of the original image
            Bitmap stegoImage = new Bitmap(image.Width, image.Height);
            using (Graphics g = Graphics.FromImage(stegoImage))
            {
                g.DrawImage(image, 0, 0);
            }

            // Convert message to binary
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            string messageBinary = string.Empty;
            
            // Add message length as a 32-bit integer at the beginning
            byte[] lengthBytes = BitConverter.GetBytes(messageBytes.Length);
            messageBinary += BytesToBinary(lengthBytes);
            
            // Add the actual message
            messageBinary += BytesToBinary(messageBytes);

            // Check if the message can fit in the image
            if (messageBinary.Length > image.Width * image.Height * 3)
            {
                throw new Exception("Message is too large for this image.");
            }

            // Lock the bitmap data for faster access
            System.Drawing.Imaging.BitmapData bmpData = stegoImage.LockBits(
                new Rectangle(0, 0, stegoImage.Width, stegoImage.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                stegoImage.PixelFormat);
            
            try
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(stegoImage.PixelFormat) / 8;
                int byteCount = bmpData.Stride * stegoImage.Height;
                byte[] pixels = new byte[byteCount];
                
                // Copy the bitmap data to the byte array
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, byteCount);
                
                int messageIndex = 0;
                
                // Embed the message
                for (int y = 0; y < stegoImage.Height && messageIndex < messageBinary.Length; y++)
                {
                    for (int x = 0; x < stegoImage.Width && messageIndex < messageBinary.Length; x++)
                    {
                        // Calculate position in the byte array
                        int pos = (y * bmpData.Stride) + (x * bytesPerPixel);
                        
                        // Modify B, G, R channels (in that order in the byte array for most formats)
                        for (int color = 0; color < 3 && messageIndex < messageBinary.Length; color++)
                        {
                            if (pos + color < pixels.Length)
                            {
                                pixels[pos + color] = SetLSB(pixels[pos + color], messageBinary[messageIndex]);
                                messageIndex++;
                            }
                        }
                    }
                }
                
                // Copy the byte array back to the bitmap
                System.Runtime.InteropServices.Marshal.Copy(pixels, 0, bmpData.Scan0, byteCount);
            }
            finally
            {
                // Unlock the bitmap data
                stegoImage.UnlockBits(bmpData);
            }

            return stegoImage;
        }

        private string ExtractMessage(Bitmap image)
        {
            string binaryMessage = string.Empty;
            int messageLength = 0;
            bool lengthExtracted = false;
            
            // Lock the bitmap data for faster access
            System.Drawing.Imaging.BitmapData bmpData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                image.PixelFormat);
            
            try
            {
                int bytesPerPixel = System.Drawing.Bitmap.GetPixelFormatSize(image.PixelFormat) / 8;
                int byteCount = bmpData.Stride * image.Height;
                byte[] pixels = new byte[byteCount];
                
                // Copy the bitmap data to the byte array
                System.Runtime.InteropServices.Marshal.Copy(bmpData.Scan0, pixels, 0, byteCount);
                
                int pixelIndex = 0;
                
                // Extract bits until we have the complete message
                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        // Calculate position in the byte array
                        int pos = (y * bmpData.Stride) + (x * bytesPerPixel);
                        
                        // Extract from B, G, R channels (in that order in the byte array for most formats)
                        for (int color = 0; color < 3; color++)
                        {
                            if (pos + color < pixels.Length)
                            {
                                binaryMessage += GetLSB(pixels[pos + color]);
                                
                                // Check if we have enough bits to extract the length
                                if (!lengthExtracted && binaryMessage.Length == 32)
                                {
                                    messageLength = BinaryToInt(binaryMessage);
                                    lengthExtracted = true;
                                }
                                
                                // Check if we have the complete message
                                if (lengthExtracted && binaryMessage.Length == 32 + messageLength * 8)
                                {
                                    // We have the complete message
                                    return Encoding.UTF8.GetString(BinaryToBytes(binaryMessage.Substring(32)));
                                }
                            }
                        }
                    }
                }
                
                // If we get here, we couldn't extract a complete message
                if (lengthExtracted)
                {
                    // We at least got the length, but not the full message
                    throw new Exception("Could not extract the complete message. The image may be corrupted or doesn't contain a valid message.");
                }
                else
                {
                    throw new Exception("Could not extract message length. The image may not contain a hidden message.");
                }
            }
            finally
            {
                // Unlock the bitmap data
                image.UnlockBits(bmpData);
            }
        }

        private byte SetLSB(byte value, char bit)
        {
            if (bit == '0')
            {
                // Clear the LSB
                return (byte)(value & 0xFE);
            }
            else
            {
                // Set the LSB
                return (byte)(value | 0x01);
            }
        }

        private char GetLSB(byte value)
        {
            return ((value & 0x01) == 0) ? '0' : '1';
        }

        private string BytesToBinary(byte[] bytes)
        {
            string binary = string.Empty;
            foreach (byte b in bytes)
            {
                binary += Convert.ToString(b, 2).PadLeft(8, '0');
            }
            return binary;
        }

        private byte[] BinaryToBytes(string binary)
        {
            int byteCount = binary.Length / 8;
            byte[] bytes = new byte[byteCount];
            
            for (int i = 0; i < byteCount; i++)
            {
                string byteBinary = binary.Substring(i * 8, 8);
                bytes[i] = Convert.ToByte(byteBinary, 2);
            }
            
            return bytes;
        }

        private int BinaryToInt(string binary)
        {
            byte[] bytes = BinaryToBytes(binary);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}