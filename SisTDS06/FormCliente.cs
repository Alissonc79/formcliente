﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace SisTDS06
{
    public partial class FormCliente : Form
    {
        private object dgvCliente;
        private object cli;
        private object cliente;

        public FormCliente()
        {
            InitializeComponent();
        }

        private void btnCep_Click(object sender, EventArgs e)
        {
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://viacep.com.br/ws/" + txtCep.Text + "/json");
                request.AllowAutoRedirect = false;
                HttpWebResponse ChecaServidor = (HttpWebResponse)request.GetResponse();
                if (ChecaServidor.StatusCode != HttpStatusCode.OK)
                {
                    MessageBox.Show("Servidor Indisponível!");
                    return; //Sai da rotina e para e codificação
                }
                using (Stream webStream = ChecaServidor.GetResponseStream())
                {
                    if (webStream != null)
                    {
                        using (StreamReader responseReader = new StreamReader(webStream))
                        {
                            string response = responseReader.ReadToEnd();
                            response = Regex.Replace(response, "[{},]", string.Empty);
                            response = response.Replace("\"", "");

                            String[] substrings = response.Split('\n');

                            int cont = 0;
                            foreach (var substring in substrings)
                            {
                                if (cont == 1)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    if (valor[0] == "  erro")
                                    {
                                        MessageBox.Show("CEP não encontrado!");
                                        txtCep.Focus();
                                        return;
                                    }
                                }

                                //Endereço
                                if (cont == 2)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtEndereco.Text = valor[1];
                                }

                                //Bairro
                                if (cont == 4)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtBairro.Text = valor[1];
                                }

                                //Cidade
                                if (cont == 5)
                                {
                                    string[] valor = substring.Split(":".ToCharArray());
                                    txtCidade.Text = valor[1];
                                }
                                cont++;
                            }
                        }
                    }
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            try
            {
                Cliente cliente = new Cliente();
                cliente.Inserir(txtNome.Text, txtCpf.Text, txtBairro.Text, txtCelular.Text, dtpDtNascimento.Value, txtCep.Text, txtEndereco.Text, txtCidade.Text, txtEmail.Text);
                MessageBox.Show("Cliente cadastrado com sucesso!", "Cadastro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = cliente.listacliente();
                dgvCli.DataSource = cli;
                txtCpf.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
            }
            catch (Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }

        private void btnLocalizar_Click(object sender, EventArgs e)
        {
              try
                {
               int cpf = Convert.ToInt32(txtCpf.Text.Trim());
               Cliente Cliente = new Cliente();
               Cliente.Localiza(cpf);
               txtCpf.Text = "";
               txtNome.Text = "";
               txtCelular.Text = "";
               this.dtpDtNascimento.Value = DateTime.Now.Date;
               txtCep.Text = "";
               txtEndereco.Text = "";
               txtCidade.Text = "";
               txtBairro.Text = "";
               txtEmail.Text = "";
            }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
                try
                {
                int cpf = Convert.ToInt32(txtCpf.Text.Trim());
                Cliente Cliente = new Cliente();
                Cliente.Exclui(cpf);
                MessageBox.Show("Cliente excluído com sucesso!", "Exclusão", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = Cliente.listacliente();
                dgvCli.DataSource = cli;
                txtCpf.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            {
                try
                {
                int cpf = Convert.ToInt32(txtCpf.Text.Trim());
                Cliente Cliente = new Cliente();
                Cliente.Atualizar(txtNome.Text, txtCpf.Text, txtBairro.Text, txtCelular.Text, dtpDtNascimento.Value, txtCep.Text, txtEndereco.Text, txtCidade.Text, txtEmail.Text);
                MessageBox.Show("cliário atualizado com sucesso!", "Edição", MessageBoxButtons.OK, MessageBoxIcon.Information);
                List<Cliente> cli = Cliente.listacliente();
                dgvCli.DataSource = cli;
                txtCpf.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
                ClassConecta.FecharConexao();
                }
                catch (Exception er)
                {
                    MessageBox.Show(er.Message);
                }
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
                txtCpf.Text = "";
                txtNome.Text = "";
                txtCelular.Text = "";
                this.dtpDtNascimento.Value = DateTime.Now.Date;
                txtCep.Text = "";
                txtEndereco.Text = "";
                txtCidade.Text = "";
                txtBairro.Text = "";
                txtEmail.Text = "";
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
 }
