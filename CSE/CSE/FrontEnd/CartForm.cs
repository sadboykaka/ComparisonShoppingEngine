﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSE.FrontEnd
{
    public partial class CartForm : Form
    {
        /// <summary>
        /// List of items added to cart.
        /// </summary>
        List<string> CartItems { get; set; }
        List<string> RemovedItems { get; set; }
        /// <summary>
        /// This form is needed in order to change listView items
        /// in initial CPPF form.
        /// </summary>
        CompareProductPricesForm compareProductPricesForm;
        public CartForm(List<string> items, CompareProductPricesForm form)
        {
            InitializeComponent();
            CartItems = items;
            compareProductPricesForm = form;
            RemovedItems = new List<string>();
        }

        private void TheCheapestStoreButton_OnClick(object sender, EventArgs e)
        {
            var result = compareProductPricesForm.formsToolKit.TheCheapestStore.GetCheapestStoreNameAndSum(listViewOfChosenProducts);
            compareProductPricesForm.formsToolKit.DisplayCheapestStoreInfo(result.Key.ToUpper(), result.Value);
        }
        private void AddItemsToListView()
        {
            foreach(var item in CartItems)
            {
                listViewOfChosenProducts.Items.Add(item);
            }
        }
        private void CartForm_Load(object sender, EventArgs e)
        {
            AddItemsToListView();

        }

        private void GoBackButton_OnClick(object sender, EventArgs e)
        {
            compareProductPricesForm.AddItemsToListView(RemovedItems);
            Hide();
        }

        private void RemoveItemButton_OnClick(object sender, EventArgs e)
        {
            foreach(ListViewItem item in listViewOfChosenProducts.CheckedItems)
            {
                RemovedItems.Add(item.Text);
                listViewOfChosenProducts.Items.Remove(item);
            }
        }
    }
}
