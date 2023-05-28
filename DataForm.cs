using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopForAdmin
{
    public partial class DataForm : Form
    {
        public AppForm Id { get; set; }
        protected TableFormCastom grid;
        protected DataBaseClass dataBase;

        public DataForm(AppForm id, DataBaseClass dataBaseClass)
        {
            InitializeComponent();
            Id = id;
            Load += (s, e) => Start();
            dataBase=dataBaseClass;
            this.FormClosed += DataForm_FormClosed;
        }

        private void DataForm_FormClosed(object? sender, FormClosedEventArgs e)
        {
            try{dataBase.SaveChanges();}
            catch (Exception ex){ MessageBox.Show(ex.Message.ToString()); }
        }
        void Start()
        {
            grid = new();
            switch(Id)
            {
                case AppForm.Product:
                    dataBase.broducts.Load();
                    grid.DataSource= dataBase.broducts.Local.ToBindingList();
                    break;
                case AppForm.Sales:
                    dataBase.sales.Load();
                    grid.DataSource = dataBase.sales.Local.ToBindingList();
                    break;
                case AppForm.Buyers:
                    dataBase.buyers.Load();
                    grid.DataSource = dataBase.buyers.Local.ToBindingList();
                    break;
            }
            Controls.Add(grid);
        }
    }
}
