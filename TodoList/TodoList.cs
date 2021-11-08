
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;



namespace TodoList
{
    public partial class TodoList : Form
    {
        public TodoList()
        {
            InitializeComponent();
        }

        private void SetListViewColumnSizes(ListView lvw, int width)
        {
            foreach (ColumnHeader col in lvw.Columns)
                col.Width = width;
        }

        DataBase dataBaseOjb = new DataBase();
        private int IdTarea = 0;

        private void TodoList_Load(object sender, EventArgs e)
        {
            DataBase dataBaseOjb = new DataBase();
            
            cargar();
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            string consulta = "insert into Tareas(tarea, descripcion) values(@tarea, @descripcion)";
            SQLiteCommand myCommand = new SQLiteCommand(consulta, dataBaseOjb.myConection);
            dataBaseOjb.OpenConecction();
            myCommand.Parameters.AddWithValue("@tarea", txtTarea.Text);
            myCommand.Parameters.AddWithValue("@descripcion", txtDescripcion.Text);
            myCommand.ExecuteNonQuery();
            dataBaseOjb.CloseConecction();

            MessageBox.Show("Se guardo su tarea", "Tareas", MessageBoxButtons.OK, MessageBoxIcon.Information);
            
            cargar();
            txtTarea.Clear();
            txtDescripcion.Clear();
        }

        public void cargar()
        {
            lvTareas.Clear();
            SQLiteDataAdapter da = new SQLiteDataAdapter("select *from tareas", dataBaseOjb.myConection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach(DataColumn itemColumn in dt.Columns)
            {
                lvTareas.Columns.Add(Convert.ToString(itemColumn));
            }

            foreach(DataRow row in dt.Rows)
            {
                var itemlist = new ListViewItem(Convert.ToString(row[0]));

                itemlist.SubItems.Add(Convert.ToString(row[1]));
                itemlist.SubItems.Add(Convert.ToString(row[2]));
                lvTareas.Items.Add(itemlist);
            }
            SetListViewColumnSizes(lvTareas, -2);
        }

        private void Delete()
        {
            string delete;
            delete = "delete from  tareas where id ='" + IdTarea + "'";
            SQLiteCommand cmd = new SQLiteCommand(delete, dataBaseOjb.myConection);
            dataBaseOjb.OpenConecction();
            cmd.ExecuteNonQuery();
            dataBaseOjb.CloseConecction();
            MessageBox.Show("Registro Eleminado correctamente");
            cargar();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (lvTareas.SelectedItems.Count > 0)
            {
                
                ListViewItem listItem = lvTareas.SelectedItems[0];

                IdTarea = Convert.ToInt32(listItem.SubItems[0].Text);
                txtTarea.Text = listItem.SubItems[1].Text;
                txtDescripcion.Text = listItem.SubItems[2].Text;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (lvTareas.SelectedItems.Count > 0)
            {

                ListViewItem listItem = lvTareas.SelectedItems[0];

                IdTarea = Convert.ToInt32(listItem.SubItems[0].Text);
                Delete();
            }
        }

        private void Actualizar()
        {
            string update;
            update = "update tareas set tarea ='" + txtTarea.Text + "',descripcion ='" + txtDescripcion.Text + "' where id            ='" + IdTarea + "'";
            SQLiteCommand cmd = new SQLiteCommand(update, dataBaseOjb.myConection);
            dataBaseOjb.OpenConecction();
            cmd.ExecuteNonQuery();
            dataBaseOjb.CloseConecction();
            MessageBox.Show("Registro actualizado correctamente");
            cargar();
            txtTarea.Clear();
            txtDescripcion.Clear();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Actualizar();
            cargar();
        }
    }
}
