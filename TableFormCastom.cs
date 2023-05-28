using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopForAdmin
{
    public class TableFormCastom:DataGridView
    {
        public TableFormCastom()
        {
            this.AutoSizeColumnsMode=DataGridViewAutoSizeColumnsMode.AllCells;
            this.Dock= DockStyle.Fill;
            this.DataError += (s, e) => MessageBox.Show(e.ToString());
        }
    }
}
