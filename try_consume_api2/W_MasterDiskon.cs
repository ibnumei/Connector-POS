using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace try_consume_api2
{
    public partial class W_MasterDiskon : Form
    {
        public W_MasterDiskon()
        {
            InitializeComponent();
        }

        public void delete_data()
        {
            String sql = "DELETE FROM discountitemselected";
            Crud del1 = new Crud();
            del1.NonReturn2(sql);

            String sql2 = "DELETE FROM discountretail";
            Crud del2 = new Crud();
            del2.NonReturn2(sql2);

            String sql3 = "DELETE FROM discountretaillines";
            Crud del3 = new Crud();
            del3.NonReturn2(sql3);

            String sql4 = "DELETE FROM customer";
            Crud del4 = new Crud();
            del4.NonReturn2(sql4);

            String sql5 = "DELETE FROM itemdimensionbrand";
            Crud del5 = new Crud();
            del5.NonReturn2(sql5);

            String sql6 = "DELETE FROM itemdimensioncolor";
            Crud del6 = new Crud();
            del6.NonReturn2(sql6);

            String sql7 = "DELETE FROM itemdimensiondepartment";
            Crud del7 = new Crud();
            del7.NonReturn2(sql7);

            String sql8 = "DELETE FROM itemdimensiondepartmenttype";
            Crud del8 = new Crud();
            del8.NonReturn2(sql8);


            String sql10 = "DELETE FROM discountstore";
            Crud del10 = new Crud();
            del10.NonReturn2(sql10);

        }
        private void b_reload_Click(object sender, EventArgs e)
        {
            delete_data();

            API_DiscountSelectedItem sel_item = new API_DiscountSelectedItem();
            sel_item.get_DiscountSelectedItem().Wait();

            API_DiscountRetail disc_retail = new API_DiscountRetail();
            disc_retail.get_cust_id();
            disc_retail.get_DiscountRetail().Wait();

            API_DiscountRetailLines disc_retail_line = new API_DiscountRetailLines();
            disc_retail_line.cek();
            //disc_retail_line.get_DiscountRetailLines().Wait();

            API_CustGroup cust_group = new API_CustGroup();
            cust_group.cek_data_log();
            cust_group.del_tabel();
            cust_group.get_cust_group().Wait();

            API_Customer cust = new API_Customer();
            cust.getCustomer().Wait();

            API_ItemDimensionBrand item_brang = new API_ItemDimensionBrand();
            item_brang.getItemBrand().Wait();

            API_ItemDimensionColor item_color = new API_ItemDimensionColor();
            item_color.getItemColor().Wait();

            API_ItemDimensionDepartment item_dept = new API_ItemDimensionDepartment();
            item_dept.getItemDepartment().Wait();

            API_ItemDimensionDepartmentType item_dept_type = new API_ItemDimensionDepartmentType();
            item_dept_type.getItemDepartmentType().Wait();

            API_DiscountStoreApi store_api = new API_DiscountStoreApi();
            store_api.getStore().Wait();
        }

        private void b_DisSelItem_Click(object sender, EventArgs e)
        {
            String sql = "DELETE FROM discountitemselected";
            Crud del1 = new Crud();
            del1.NonReturn2(sql);

            API_DiscountSelectedItem sel_item = new API_DiscountSelectedItem();
            sel_item.get_DiscountSelectedItem().Wait();
        }

        private void b_DisRetail_Click(object sender, EventArgs e)
        {
            String sql2 = "DELETE FROM discountretail";
            Crud del2 = new Crud();
            del2.NonReturn2(sql2);

            API_DiscountRetail disc_retail = new API_DiscountRetail();
            disc_retail.get_cust_id();
            disc_retail.get_DiscountRetail().Wait();
        }

        private void b_DisRetailLines_Click(object sender, EventArgs e)
        {
            String sql3 = "DELETE FROM discountretaillines";
            Crud del3 = new Crud();
            del3.NonReturn2(sql3);

            API_DiscountRetailLines disc_retail_line = new API_DiscountRetailLines();
            disc_retail_line.cek();
            //disc_retail_line.get_DiscountRetailLines().Wait();

        }

        private void b_cust_Click(object sender, EventArgs e)
        {
            String sql4 = "DELETE FROM customer";
            Crud del4 = new Crud();
            del4.NonReturn2(sql4);

            API_Customer cust = new API_Customer();
            cust.getCustomer().Wait();
        }

        private void b_DimBrand_Click(object sender, EventArgs e)
        {
            String sql5 = "DELETE FROM itemdimensionbrand";
            Crud del5 = new Crud();
            del5.NonReturn2(sql5);

            API_ItemDimensionBrand item_brang = new API_ItemDimensionBrand();
            item_brang.getItemBrand().Wait();
        }

        private void b_DimColor_Click(object sender, EventArgs e)
        {
            String sql6 = "DELETE FROM itemdimensioncolor";
            Crud del6 = new Crud();
            del6.NonReturn2(sql6);

            API_ItemDimensionColor item_color = new API_ItemDimensionColor();
            item_color.getItemColor().Wait();

        }

        private void b_DimDept_Click(object sender, EventArgs e)
        {
            String sql7 = "DELETE FROM itemdimensiondepartment";
            Crud del7 = new Crud();
            del7.NonReturn2(sql7);

            API_ItemDimensionDepartment item_dept = new API_ItemDimensionDepartment();
            item_dept.getItemDepartment().Wait();
        }

        private void b_DimDeptType_Click(object sender, EventArgs e)
        {
            String sql8 = "DELETE FROM itemdimensiondepartmenttype";
            Crud del8 = new Crud();
            del8.NonReturn2(sql8);

            API_ItemDimensionDepartmentType item_dept_type = new API_ItemDimensionDepartmentType();
            item_dept_type.getItemDepartmentType().Wait();
        }

        private void b_DiscStore_Click(object sender, EventArgs e)
        {
            String sql10 = "DELETE FROM discountstore";
            Crud del10 = new Crud();
            del10.NonReturn2(sql10);

            API_DiscountStoreApi store_api = new API_DiscountStoreApi();
            store_api.getStore().Wait();
        }

        private void b_custGroup_Click(object sender, EventArgs e)
        {
            //API_CustGroup cust_group = new API_CustGroup();
            //cust_group.cek_data_log();
            //cust_group.del_tabel();
            //cust_group.get_cust_group().Wait();

            String sql11 = "DELETE FROM customer_group";
            Crud del11 = new Crud();
            del11.NonReturn2(sql11);

            API_CustGroup cust_group = new API_CustGroup();
            cust_group.get_cust_group().Wait();
        }
    }
}
