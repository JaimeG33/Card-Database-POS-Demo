using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo_Application_1.Forms
{
    public partial class BulkInvEntry : Form
    {
        public BulkInvEntry()
        {
            InitializeComponent();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BulkInvEntry_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'revised_Demo_Database_CAv2DataSet.PokemonInventory' table. You can move, or remove it, as needed.
            this.pokemonInventoryTableAdapter.Fill(this.revised_Demo_Database_CAv2DataSet.PokemonInventory);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
