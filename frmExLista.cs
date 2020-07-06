using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace ExLIsta
{
    public partial class frmExLista : Form
    {
        SqlConnection con = new SqlConnection("Data Source=aula2020.database.windows.net;Initial Catalog=DataGrid;User ID=tds02;Password=@nuvem2020;Connect Timeout=60;Encrypt=True;MultipleActiveResultSets=true;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
        List<Pessoa> pLista = null;
        public frmExLista()
        {
            InitializeComponent();
        }

        //Adicionar objetos a minha lista
        private void CarregaLista()
        {
            pLista = new List<Pessoa>();
            pLista.Add(new Pessoa(1, "João", 12, 'M'));
            pLista.Add(new Pessoa(2, "Maria", 12, 'F'));
            pLista.Add(new Pessoa(3, "Roberto", 89, 'M'));
            pLista.Add(new Pessoa(4, "Mari", 33, 'F'));
            pLista.Add(new Pessoa(5, "Rodrigo", 47, 'M'));
            pLista.Add(new Pessoa(6, "Helena", 41, 'F'));
        }

        void Imprimir(List<Pessoa> pLista, string info)
        {
            lbResultado.Items.Clear();
            lbResultado.Items.Add(info);
            lbResultado.Items.Add("");
            lbResultado.Items.Add("ID\tNome\tIdade\tSexo");
            pLista.ForEach(delegate (Pessoa p)
            {
                lbResultado.Items.Add(p.ID + "\t" + p.Nome + "\t" + p.Idade + "\t" + p.Sexo);
            });
        }

        private void btnMostrar_Click(object sender, EventArgs e)
        {
            CarregaLista();
            Imprimir(pLista, "Mostrando a Lista");
        }

        private void btnFiltro_Click(object sender, EventArgs e)
        {
            List<Pessoa> FiltraIdade = pLista.FindAll(delegate (Pessoa p) {
                return p.Idade > 30;
            });
            Imprimir(FiltraIdade, "Pessoas acima de 30 anos");
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            List<Pessoa> RemoveM = pLista;
            RemoveM.RemoveAll(delegate (Pessoa p) { return p.Sexo == 'M'; });
            Imprimir(RemoveM, "Removendo todas as pessoas do sexo Masculino");
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
            List<Pessoa> FiltraIdade = pLista.FindAll(delegate (Pessoa p1) { return p1.Idade == 12; });
            Imprimir(FiltraIdade, "Pessoas com 12 anos");
        }

        private void btnLocalizar2_Click(object sender, EventArgs e)
        {
            Int32 Lid = Convert.ToInt32(txtId.Text);
            Pessoa pessoas = pLista.Find(delegate (Pessoa p1) { return p1.ID == Lid; });
            lbResultado.Items.Add("Pessoa Localizada");
            lbResultado.Items.Add(pessoas.ID + "\t" + pessoas.Nome + "\t" + pessoas.Idade + "\t" + pessoas.Sexo);
            txtNome.Text = pessoas.Nome;
            txtIdade.Text = Convert.ToString(pessoas.Idade);
            txtSexo.Text = Convert.ToString(pessoas.Sexo);
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            List<Pessoa> npessoa = pLista;
            npessoa.Add(new Pessoa() { ID = Convert.ToInt32(txtId.Text.Trim()), Nome = txtNome.Text.Trim(), Idade = Convert.ToSByte(txtIdade.Text.Trim()), Sexo = Convert.ToChar(txtSexo.Text.Trim()) });
            Imprimir(npessoa, "Pessoa Adicionada");
            txtId.Text = "";
            txtNome.Text = "";
            txtIdade.Text = "";
            txtSexo.Text = "";
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Int32 Lid = Convert.ToInt32(txtId.Text);
            List<Pessoa> RemoveLista = pLista;
            RemoveLista.RemoveAll(delegate (Pessoa p) { return p.ID == Lid; });
            Imprimir(RemoveLista, "Pessoa Removida");
        }

        private void btnGravaBD_Click(object sender, EventArgs e)
        {
            List<Pessoa> ex = pLista;
            for (int i = 0; i < ex.Count; i++)
            {
                SqlCommand cmd = new SqlCommand("Inserir_Pessoa", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@Id", SqlDbType.Int).Value = ex[i].ID;
                cmd.Parameters.Add("@nome", SqlDbType.NChar).Value = ex[i].Nome;
                cmd.Parameters.Add("@idade", SqlDbType.SmallInt).Value = ex[i].Idade;
                cmd.Parameters.Add("@sexo", SqlDbType.Char).Value = ex[i].Sexo;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            MessageBox.Show("Lista gravada com sucesso!", "Gravação", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            Int32 Lid = Convert.ToInt32(txtId.Text);
            List<Pessoa> ed = pLista;
            var edpessoa = ed.FirstOrDefault(x => x.ID == Lid);
            if (edpessoa !=null)
            {
                edpessoa.ID = Convert.ToInt32(txtId.Text);
                edpessoa.Nome = txtNome.Text;
                edpessoa.Idade = Convert.ToSByte(txtIdade.Text);
                edpessoa.Sexo = Convert.ToChar(txtSexo.Text);
            }
            lbResultado.Items.Clear();
            Imprimir(pLista, "Lista Editada");
            txtId.Text = "";
            txtNome.Text = "";
            txtIdade.Text = "";
            txtSexo.Text = "";
        }
    }
}