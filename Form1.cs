namespace ShopForAdmin
{
    public partial class Form1 : Form
    {
        DataBaseClass dataBase;
        MenuStripP menuStripP;
        AppForm appForm;
        public Form1()
        {
            InitializeComponent();
            dataBase = new ContextFactory().CreateDbContext(null);
            menuStripP = new MenuStripP() {Open=OpenInnerForm,
            Save=Save,
            Exit=()=>this.Close()};
            this.IsMdiContainer = true;
            Controls.AddRange(new Control[] { menuStripP });
            this.FormClosing +=(s,e)=>Save();

        }

        void Save() => dataBase.SaveChanges();
        void OpenInnerForm(AppForm id)
        {
           DataForm dataForm=MdiChildren.Select(f=>f as DataForm).FirstOrDefault(f=>f.Id==id);
            if (dataForm==null) 
            {
                dataForm = new(id,dataBase);
                dataForm.MdiParent = this;
                dataForm.Show();
            }
            if(dataForm.WindowState==FormWindowState.Minimized) 
            { dataForm.WindowState = FormWindowState.Normal; }
            dataForm.Activate();
        }
    }
}