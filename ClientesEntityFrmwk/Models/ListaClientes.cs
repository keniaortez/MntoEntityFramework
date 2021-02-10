using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace ClientesEntityFrmwk.Models
{
    public class ListaClientes
    {
        public List<Cliente> clie { get; set; }
        public ListaClientes()
        {

        }

        //Buscar clientes
        public Boolean BuscarClientes(int IdCliente)
        {
            Boolean resp = false;
            try
            {
                string Cnstr = System.Configuration.ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;
                DataSet dsCliente = new DataSet();
                SqlConnection Cnn = new SqlConnection(Cnstr);
                string sql = "BuscarClientes";
                SqlDataAdapter da = new SqlDataAdapter(sql, Cnn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", IdCliente);
                da.Fill(dsCliente);
                if (dsCliente.Tables[0].Rows.Count > 0) 
                {
                    clie = new List<Cliente>();
                    foreach (DataRow dr in dsCliente.Tables[0].Rows) 
                    {
                        string _FechaModificacion = "";
                        int _IdCliente = int.Parse(dr["IdCliente"].ToString().Trim());
                        string _Identificacion = dr["Identificacion"].ToString().Trim();
                        string _PrimerNombre = dr["PrimerNombre"].ToString().Trim();
                        string _PrimeroApellido = dr["PrimerApellido"].ToString().Trim();
                        string _Edad = dr["Edad"].ToString().Trim();
                        DateTime _FechaCreacion = DateTime.Parse(dr["FechaCreacion"].ToString().Trim());
                        if (dr["FechaModificacion"].ToString().Trim() != "")
                        {
                            _FechaModificacion = dr["FechaModificacion"].ToString().Trim();                            
                        }

                        Cliente objCliente = new Cliente(_IdCliente, _Identificacion, _PrimerNombre, _PrimeroApellido, _Edad, _FechaCreacion, _FechaModificacion);
                        clie.Add(objCliente);
                        resp = true;
                    }
                }
            }
            catch (Exception ex)
            {
                resp = false;
            }
            
            return resp;
        
        } 


    }
}