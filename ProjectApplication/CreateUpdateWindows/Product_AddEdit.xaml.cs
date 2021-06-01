﻿using ProjectApplication.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProjectApplication.CreateUpdateWindows
{
    /// <summary>
    /// Interaction logic for Product_AddEdit.xaml
    /// </summary>
    public partial class Product_AddEdit : Window
    {
        public ProjectApplicationContext Ctx { get; set; }

        public ObservableCollection<Product> Products { get; set; }

        public Product SelectedProduct { get; set; }


        public Product_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Product> products)
        {
            Ctx = ctx;
            Products = products;
            InitializeComponent();
        }

        public Product_AddEdit(ProjectApplicationContext ctx, ObservableCollection<Product> products, Product SelectedProduct)
        {
            Ctx = ctx;
            Products = products;
            SelectedProduct = SelectedProduct;
            InitializeComponent();
        }
    }
}
