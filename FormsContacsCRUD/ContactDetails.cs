﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsContacsCRUD
{
    public partial class ContactDetails : Form
    {
        private BusinessLogLayer _businessLogLayer;
        private Contact _contact;
        public ContactDetails()
        {
            InitializeComponent();
            _businessLogLayer = new BusinessLogLayer();
        }


        #region EVENTS

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveContact();
            this.Close();
            ((Main)this.Owner).PopulateContacts();
        }

        #endregion

        #region PUBLIC METHODS

        public void LoadContact(Contact contact)
        {
            _contact = contact;
            if (contact != null)
            {
                ClearForm();

                txtFirstName.Text = contact.FirstName;
                txtLastName.Text = contact.LastName;
                txtPhone.Text = contact.Phone;
                txtAdress.Text = contact.Address;

            }
        }

        #endregion


        #region PRIVATE METHODS



        private void SaveContact()
        {
            Contact contact = new Contact();
            contact.FirstName = txtFirstName.Text;
            contact.LastName = txtLastName.Text;
            contact.Phone = txtPhone.Text;
            contact.Address = txtAdress.Text;

            contact.Id = _contact != null ? _contact.Id : 0;

            _businessLogLayer.SaveContact(contact);
        }

       
        
        private void ClearForm()
        {
            txtFirstName.Text = string.Empty;
            txtLastName.Text = string.Empty;
            txtPhone.Text = string.Empty;
            txtAdress.Text = string.Empty;
        }
    }
        #endregion
}


    