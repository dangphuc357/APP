﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DTO;
using component;

namespace GUI.ProductGUI
{
    public partial class FormProductList : Form
    {
        ProductBLL productBLL = new ProductBLL();
        public FormProductList()
        {
            InitializeComponent();
        }

        private void FormProductList_Load(object sender, EventArgs e)
        {
            panel_Products.AutoScroll = true;

            List<sanpham> products = productBLL.getProduct();

            int xOffset = 0; 
            int yOffset = 0; 
            int itemWidth = 269; 
            int itemHeight = 85; 
            int spacing = 2; 

            panel_Products.SuspendLayout();
            foreach (var product in products)
            {
                UC_ProductCart productItem = new UC_ProductCart
                {
                    MaSP = product.MaSP,
                    TenSP = product.TenSP,
                    Gia = (decimal)product.Gia,
                    Sale = (decimal)product.PhanTramGiamGia,
                    Size = new System.Drawing.Size(itemWidth, itemHeight), 
                };
                productItem.DetailButtonClick += ProductItem_DetailButtonClick; ;
                productItem.SetFormProductDetail(typeof(FormProductDetail));
                //var image = imageService.GetFirstImageByMaSP(product.MaSP);
                //if (image != null)
                //{
                //    productItem.HinhAnh = image.Url_Img;
                //}


                if (xOffset + itemWidth > panel_Products.Width)
                {
                    xOffset = 0;
                    yOffset += itemHeight + spacing; 
                }
                productItem.Location = new Point(xOffset, yOffset);
                xOffset += itemWidth + spacing;
                panel_Products.Controls.Add(productItem);
            }

            panel_Products.ResumeLayout();

        }

        private void ProductItem_DetailButtonClick(object sender, int maSP)
        {
            Form parentForm = this.FindForm();

            Panel overlayPanel = new Panel
            {
                Size = parentForm.ClientSize,
                Location = new Point(0, 0),
                BackColor = Color.FromArgb(128, 0, 0, 0) 
            };

            parentForm.Controls.Add(overlayPanel);
            overlayPanel.BringToFront(); 

            FormProductDetail form = new FormProductDetail(maSP);

            form.FormClosed += (s, args) =>
            {
                parentForm.Controls.Remove(overlayPanel); 
            };

            form.ShowDialog();
        }

        private void panel_Products_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
