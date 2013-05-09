using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using MySql;

namespace dist_sys_ass_2
{
    public partial class _default : System.Web.UI.Page
    {
        public dstempEntities1 entities;
        public dataEntities1 members;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                this.entities = new dstempEntities1();
                this.members = new dataEntities1();
            }
            catch
            {
                this.entities = null;
                this.members = null;
            }

            ////////////////////////////
            // _INIT_LOAD_MEMBERS(); //
            ///////////////////////////
            ///////////////////////////
            /* for the love of jesus don't run this again! */
        }


        /// <summary>
        /// use this function and die a horribly slow and painful death.
        /// </summary>
        protected void _INIT_LOAD_MEMBERS()
        {
            //regular members
            var employees = from emp in this.entities.employees
                            where
                                ((DateTime.Now.Year - emp.birth_date.Year) <= 67 && (DateTime.Now.Year - emp.birth_date.Year) <= 60)
                            select emp;
            foreach (employee emp in employees)
            {
                this.members.memberships.AddObject(new membership
                {
                    FirstName = emp.first_name,
                    LastName = emp.last_name,
                    MembershipClass = "regular",
                    VerificationCode = (emp.birth_date.GetHashCode() + emp.salaries.GetHashCode()).ToString()
                });
            }
            this.members.SaveChanges();

            //get percentages
            //get salaries
            var salaries = from s in this.entities.salaries where s.to_date.Year == 9999 select s.salary1;
            employees = from e in this.entities.employees select e;
            var TT = 86220;
            var TF = 69804;

            var ett = (from e in employees where 
                          (e.salaries.Where(i => i.to_date.Year == 9999).FirstOrDefault().salary1 > TT) 
                      select e);

            var etf = (from e in employees
                      where
                          (e.salaries.Where(i => i.to_date.Year == 9999).FirstOrDefault().salary1 < TT &&
                          e.salaries.Where(i => i.to_date.Year == 9999).FirstOrDefault().salary1 > TF)
                      select e);

            //var hash = DateTime.Now.GetHashCode().ToString();

            foreach (employee e in ett)
            {
                this.members.memberships.AddObject(new membership
                {
                    FirstName = e.first_name,
                    LastName = e.last_name,
                    MembershipClass = "gold",
                    VerificationCode = (e.salaries.GetHashCode() + e.birth_date.GetHashCode()).ToString()
                });
            }

            this.members.SaveChanges();

            foreach (employee e in etf)
            {
                this.members.memberships.AddObject(new membership
                {
                    FirstName = e.first_name,
                    LastName = e.last_name,
                    MembershipClass = "silver",
                    VerificationCode = (e.salaries.GetHashCode() + e.birth_date.GetHashCode()).ToString()
                });
            }

            this.members.SaveChanges();
        }

        protected void Clear(object sender, EventArgs e)
        {
            tb_ID.Text = "";
            tb_fname.Text = "";
            tb_lname.Text = "";
            tb_memtype.Text = "";
            tb_vcode.Text = "";

            GridView1.DataSource = null;
            DataBind();
        }

        protected void Search(object sender, EventArgs e)
        {
            var search = from emp in this.members.memberships
                         where
                             (emp.FirstName == tb_fname.Text) ||
                             (emp.LastName == tb_lname.Text) ||
                             (emp.MembershipClass == tb_memtype.Text) ||
                             (emp.VerificationCode == tb_vcode.Text)
                         select emp;

            GridView1.DataSource = search;
            DataBind();
        }

        protected void Add(object sender, EventArgs e)
        {
            membership m = new membership { FirstName = tb_fname.Text, LastName = tb_lname.Text, 
                MembershipClass = tb_memtype.Text, VerificationCode = tb_vcode.Text };
            members.memberships.AddObject(m);
            members.SaveChanges();

            //can't reuse Search() method, so lets just dump the important stuff here
            var search = from emp in this.members.memberships
                         where
                             (emp.FirstName == tb_fname.Text) ||
                             (emp.LastName == tb_lname.Text) ||
                             (emp.MembershipClass == tb_memtype.Text) ||
                             (emp.VerificationCode == tb_vcode.Text)
                         select emp;

            GridView1.DataSource = search;
            DataBind();
        }

        protected void Delete(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tb_ID.Text);
            membership m = members.memberships.First(i => i.MembershipId == id);
            members.memberships.DeleteObject(m);
            members.SaveChanges();

            //attempt to search for the ID of the deleted member, should return nothing in GridView
            var ds = from mem in members.memberships where mem.MembershipId == id select mem;
            GridView1.DataSource = ds;
            DataBind();

            //ripped out of Clear() method, just cleans up the textboxes.
            tb_ID.Text = "";
            tb_fname.Text = "";
            tb_lname.Text = "";
            tb_memtype.Text = "";
            tb_vcode.Text = "";
        }

        protected void Modify(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(tb_ID.Text); //this line is needed to bypass ado.net restrictions
            membership m = members.memberships.First(i => i.MembershipId == id); //fetch the member row
            //do our update
            m.FirstName = tb_fname.Text;
            m.LastName = tb_lname.Text;
            m.MembershipClass = tb_memtype.Text;
            m.VerificationCode = tb_vcode.Text;
            members.SaveChanges(); //make changes atomic

            //update gridview to show only updated member
            var ds = from mem in members.memberships where mem.MembershipId == id select mem;
            GridView1.DataSource = ds;
            DataBind();
        }

        protected void Update(object sender, EventArgs e)
        {
            tb_ID.Text = GridView1.SelectedRow.Cells[1].Text;
            tb_fname.Text = GridView1.SelectedRow.Cells[2].Text;
            tb_lname.Text = GridView1.SelectedRow.Cells[3].Text;
            tb_memtype.Text = GridView1.SelectedRow.Cells[4].Text;
            tb_vcode.Text = GridView1.SelectedRow.Cells[5].Text;
        }
    }
}