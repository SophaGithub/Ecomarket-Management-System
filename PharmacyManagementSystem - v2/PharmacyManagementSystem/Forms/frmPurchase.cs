﻿using PharmacyManagementSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PharmacyManagementSystem.Forms
{
    public partial class frmPurchase : Form
    {
        //product
        private string productFileName = "product";
        private List<ClsProduct> Products = new List<ClsProduct>();

        //customer
        private string customerFileName = "customer";
        private List<ClsCustomer> Customers = new List<ClsCustomer>();

        //employee
        private string employeeFileName = "employee";
        private List<ClsEmployee> Employees = new List<ClsEmployee>();

        //sale-summaries
        private string saleSummariesFileName = "sale";
        private List<ClsSaleSummaries> SaleSummaries = new List<ClsSaleSummaries>();

        //sale-details
        private string saleDetailFileName = "sale_detail";
        private List<ClsSaleDetail> SaleDetails = new List<ClsSaleDetail>();

        //stock
        private string stockFileName = "stock";
        private List<ClsStock> Stocks = new List<ClsStock>();

        private IOManager iOManager = new IOManager();
        private PurchaseUtil PurchaseUtil = new PurchaseUtil();
        public frmPurchase()
        {
            InitializeComponent();
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            Products = PurchaseUtil.LoadProduct(productFileName);
            Customers = PurchaseUtil.LoadCustomer(customerFileName);
            Employees = PurchaseUtil.LoadEmployee(employeeFileName);

            SaleSummaries = PurchaseUtil.LoadSaleSummaries(saleSummariesFileName);
            SaleDetails = PurchaseUtil.LoadSaleDetails(saleDetailFileName);
            Stocks = PurchaseUtil.LoadStocks(stockFileName);

            foreach (ClsProduct p in Products)
                cmbProduct.Items.Add(p.Name);
            foreach (ClsCustomer c in Customers)
                cmbCustomer.Items.Add(c.Name);
            foreach (ClsEmployee c in Employees)
                cmbEmployee.Items.Add(c.Name);
            //PurchaseUtil.InsertStockSampleData(Products, stockFileName);
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ClsSaleItem item = new ClsSaleItem();
            if (string.IsNullOrEmpty(cmbCustomer.Text) || string.IsNullOrEmpty(cmbEmployee.Text)|| string.IsNullOrEmpty(cmbProduct.Text))
            {
                MessageBox.Show("please input all field");
                return;
            }
            item.ProductName = cmbProduct.Text;
            item.Price = double.Parse(txtCostPrice.Text);
            item.Quantity = int.Parse(txtQuantity.Text);
            item.TotalPrice = double.Parse(txtTotalPrice.Text);
            item.Unit = lblUnit.Text;

            Sales.Add(item);
            AddToDataGridView();
            Clear();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            Remove();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            if (dgvItem.Rows.Count <= 0)
            {
                MessageBox.Show("please add some items");
            }

            //save sale summaries
            ClsSaleSummaries sale = new ClsSaleSummaries();
            sale.InvoiceId = GetInvoiceId();
            sale.EmployeeId = GetEmployeeId();
            sale.CustomerId = GetCustomerId();
            sale.TotalPrice = GetTotalPrice();
            SaleSummaries.Add(sale);
            iOManager.Save(SaleSummaries, saleSummariesFileName);

            //save sale detail
            foreach (ClsSaleItem item in Sales)
            {
                ClsSaleDetail sd = new ClsSaleDetail();
                sd.InvoiceId = sale.InvoiceId;
                sd.ProductId = GetProductId(item.ProductName);
                sd.Quantity = item.Quantity;
                sd.Price = item.Price;
                sd.TotalPrice = item.TotalPrice;
                SaleDetails.Add(sd);
            }
            iOManager.Save(SaleDetails, saleDetailFileName);

            //update stock
            foreach (ClsSaleItem item in Sales)
            {
                int productId = GetProductId(item.ProductName);
                ClsStock stock = Stocks.Where(s => s.ProductId == productId).FirstOrDefault();
                if (stock != null)
                    stock.Quantity -= item.Quantity;
            }
            iOManager.Save(Stocks, stockFileName);

            MessageBox.Show("ordered!");
            ClearAll();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            frmPurchaseView view = new frmPurchaseView();
            view.purchaseView = this;
            view.ShowDialog();
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearAll();
        }

        //method
        private int GetInvoiceId()
        {
            ClsSaleSummaries sale = SaleSummaries.OrderByDescending(s => s.InvoiceId).FirstOrDefault();
            if (sale != null)
                return sale.InvoiceId + 1;
            return 1;
        }

        private void Clear()
        {
            cmbProduct.Text = "";
            txtCostPrice.Text = "0";
            txtQuantity.Text = "0";
            txtTotalPrice.Text = "0";
            lblUnit.Text = "";
            lblStock.Text = "0";
        }


        private List<ClsSaleItem> Sales = new List<ClsSaleItem>();
        

        private void AddToDataGridView()
        {
            dgvItem.Rows.Clear();
            int no = 1;
            foreach (ClsSaleItem item in Sales)
            {
                dgvItem.Rows.Add(no,
                                item.ProductName,
                                item.Quantity,
                                item.Price,
                                item.TotalPrice,
                                item.Unit);
                no++;
            }
        }

        private void Remove()
        {
            if (dgvItem.SelectedRows.Count > 0)
            {
                string prodName = dgvItem.SelectedRows[0].Cells[1].Value.ToString();
                for (int i = 0; i < Sales.Count; i++)
                {
                    ClsSaleItem sale = Sales[i];
                    if (sale.ProductName == prodName)
                    {
                        Sales.Remove(sale);
                    }
                }

                AddToDataGridView();
            }
        }

        private void ClearAll()
        {
            cmbCustomer.Text = "";
            cmbEmployee.Text = "";
            Clear();
            dgvItem.Rows.Clear();
            Sales.Clear();

            txtInvoiceId.Text = GetInvoiceId().ToString();
        }
        

        private int GetProductId(string name)
        {
            ClsProduct prod = Products.Where(p => p.Name == name).FirstOrDefault();
            if (prod != null)
                return prod.Id;
            return -1;
        }
        private double GetTotalPrice()
        {
            double totalPriceAllProduct = 0;
            foreach (ClsSaleItem item in Sales)
            {
                totalPriceAllProduct += item.TotalPrice;
            }
            return totalPriceAllProduct;
        }
        private int GetEmployeeId()
        {
            ClsEmployee emp = Employees.Where(e => e.Name == cmbEmployee.Text).FirstOrDefault();
            if (emp != null)
                return emp.Id;
            return -1;
        }
        private int GetCustomerId()
        {
            ClsCustomer cus = Customers.Where(c => c.Name == cmbCustomer.Text).FirstOrDefault();
            if (cus != null)
                return cus.Id;
            return -1;
        }


        public void RefreshPosUI(int invoiceId)
        {
            ClearAll();
            var sds = SaleDetails.Where(sd => sd.InvoiceId == invoiceId);
            if (sds != null)
            {
                foreach (var sd in sds)
                {
                    ClsSaleItem item = new ClsSaleItem();
                    item.ProductName = GetProductName(sd.ProductId);
                    item.Quantity = sd.Quantity;
                    item.Price = sd.Price;
                    item.TotalPrice = sd.TotalPrice;
                    item.Unit = GetProductUnit(sd.ProductId);
                    Sales.Add(item);
                }
            }
            AddToDataGridView();
        }

        private string GetProductUnit(int id)
        {
            ClsProduct prod = Products.Where(p => p.Id == id).FirstOrDefault();
            if (prod != null)
                return prod.Unit;
            return "";
        }
        private string GetProductName(int id)
        {
            ClsProduct prod = Products.Where(p => p.Id == id).FirstOrDefault();
            if (prod != null)
                return prod.Name;
            return "";
        }

        private void cmbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            string product = cmbProduct.Text;
            ClsProduct temp = Products.Where(p => p.Name == product).FirstOrDefault();
            if (temp != null)
            {
                int defaultUnit = 1;
                txtCostPrice.Text = temp.SellingPrice.ToString();
                lblUnit.Text = temp.Unit.ToString();
                txtQuantity.Text = defaultUnit.ToString();
                txtTotalPrice.Text = (temp.SellingPrice * defaultUnit).ToString();

                ClsStock stock = Stocks.Where(s => s.ProductId == temp.Id).FirstOrDefault();
                if (stock != null)
                    lblStock.Text = stock.Quantity.ToString();
            }
        }

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuantity.Text))
                return;

            int qty = int.Parse(txtQuantity.Text);
            string proName = cmbProduct.Text;
            ClsProduct prod = Products.Where(p => p.Name == proName).FirstOrDefault();
            if (prod != null)
            {
                double totalPrice = qty * prod.SellingPrice;
                txtTotalPrice.Text = totalPrice.ToString();
            }
        }

        private void cmbCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string customer = cmbCustomer.Text;
            ClsCustomer temp = Customers.Where(c => c.Name == customer).FirstOrDefault();
            if (temp != null)
            {
                lblPhone.Text = temp.Phone;
                lblAddress.Text = temp.Address;
            }
        }
    }
}
