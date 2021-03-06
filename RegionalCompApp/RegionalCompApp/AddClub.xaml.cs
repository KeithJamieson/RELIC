﻿
using RegionalCompApp;
using System;
using System.Windows;
using System.Windows.Controls;

namespace RegionalCompApp
{
    /// <summary>
    /// Interaction logic for AddClub.xaml
    /// </summary>
    public partial class AddClub : Window
    {

        RELICEntities db = new RELICEntities();
        string DRgrade;
        string SJgrade;
        string XCgrade;
        int airc_id;
        int club_id;
        string member_status = "A"; //Club Secretaries automatically approved
        string role = "S"; //Role sceretary is assigned
        public AddClub()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            string clubname = tbxClubName.Text.Trim();
            airc_id = Convert.ToInt16(tbxAIRC_ID.Text.Trim());


            RegisterClub(clubname);
            RegisterUser(airc_id, tbxUsername.Text.Trim(), tbxPassword.Password.Trim());
            RegisterMember(
                airc_id,
                club_id,
                member_status, //Approved 
                role, // Secretary
                tbxFirstName.Text.Trim(),
                tbxLastName.Text.Trim(),
                DRgrade,
                SJgrade,
                XCgrade,
                tbxPhone.Text.Trim(),
                tbxEmail.Text.Trim()
                );
            int result = db.SaveChanges();
            if (result > 0)
            {
                MessageBox.Show($"Club  {clubname} has been added. Secretary is {tbxFirstName.Text.Trim()}  { tbxLastName.Text.Trim()} ");
            }
            else
            {
                // db.Entry(newclub).State = System.Data.Entity.EntityState.Deleted;
                MessageBox.Show($"There was an error in Saving the Data. Please Contact Support ");
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RegisterClub(string clubname)
        {
            Club newclub = new Club();
            newclub.clubname = clubname;
            db.Entry(newclub).State = System.Data.Entity.EntityState.Added;
            db.SaveChanges();
            club_id = db.Entry(newclub).Entity.club_id;

        }

        private void DeleteClub(int club_id)
        {
            Club newclub = new Club();
            newclub.club_id = club_id;
            db.Entry(newclub).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();


        }
        private void RegisterUser(int airc_id, string username, string password)
        {


            User user = new User();
            user.airc_id = airc_id;
            user.username = username;
            user.userpassword = password;
            db.Entry(user).State = System.Data.Entity.EntityState.Added;
        }

        private void RegisterMember(int airc_id, int club_id, string memberStatus, string role, string firstname, string lastname, string DR, string SJ, string XC, string phone, string email)
        {

            Member member = new Member();
            member.airc_id = airc_id;
            member.club_id = club_id;
            member.role = role;
            member.first_name = firstname;
            member.last_name = lastname;
            member.member_status = memberStatus;
            member.DR = DR;
            member.SJ = SJ;
            member.XC = XC;
            member.phone = phone;
            member.email = email;

            db.Entry(member).State = System.Data.Entity.EntityState.Added;


        }

        private void CboDR_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboDR.SelectedItem;
            DRgrade = item.Content.ToString();

        }

        private void cboSJ_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboSJ.SelectedItem;
            SJgrade = item.Content.ToString();
        }



        private void CboXC_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBoxItem = (ComboBox)sender;
            ComboBoxItem item = (ComboBoxItem)cboXC.SelectedItem;
            XCgrade = item.Content.ToString();
        }


        //private void SaveClub(Club)
        //{
        //    db.Entry(Club).State = System.Data.Entity.EntityState.Added;
        //    //            db.Entry(Member).State = System.Data.Entity.EntityState.Added;
        //    db.SaveChanges();
        //}
    }
}
