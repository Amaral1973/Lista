using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExLIsta
{
    public partial class frmExLista : Form
    {
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
            pLista.Add(new Pessoa(7, "Mario", 23, 'M'));
            pLista.Add(new Pessoa(8, "Joana", 72, 'F'));
            pLista.Add(new Pessoa(9, "Alberto", 93, 'M'));
            pLista.Add(new Pessoa(10, "Gabi", 22, 'F'));
            pLista.Add(new Pessoa(11, "Joaquim", 15, 'M'));
            pLista.Add(new Pessoa(12, "Carla", 19, 'F'));
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
            Pessoa pessoa = pLista.Find(delegate (Pessoa p1) { return p1.Idade == 12; });
            lbResultado.Items.Add("Pessoa localizada com idade igual a 12 anos");
            lbResultado.Items.Add(pessoa.ID + "\t" + pessoa.Nome + "\t" + pessoa.Idade + "\t" + pessoa.Sexo);
        }
    }
}
