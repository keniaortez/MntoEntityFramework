using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace ClientesEntityFrmwk.Models
{
    public class Cliente
    {
        string Cnstr = System.Configuration.ConfigurationManager.ConnectionStrings["CnnStr"].ConnectionString;

        [Display(Name = "Id Cliente")]
        public int _IdCliente { get; set; }

        [Display(Name = "Identificación")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]        
        public string _Identificacion { get; set; }

        [Display(Name = "Primer Nombre")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [RegularExpression("^[A-ZñÑÁáéÉÍíÓóÚú]+$", ErrorMessage = "* Solo se permiten letras *")]
        public string _PrimerNombre { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [RegularExpression("^[A-ZñÑÁáéÉÍíÓóÚú]+$", ErrorMessage = "* Solo se permiten letras *")]
        public string _PrimeroApellido { get; set; }

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        //[RegularExpression("^[1-9]*$", ErrorMessage = "* Solo se permiten números *")]
        //[StringLength(9)]
        public int _Edad { get; set; }


        [Display(Name = "Fecha de creación")]
        public DateTime _FechaCreacion { get; set; }


        [Display(Name = "Fecha de modificación")]
        public string _FechaModificacion { get; set; }



        


        public Cliente()
        {
        }
        public Cliente(int idCliente, string Identificacion, string PrimerNombre, string PrimerApellido,  string Edad, DateTime FechaCreacion, string FechaModificacion)
        {
            try
            {
                _IdCliente = idCliente;
                _Identificacion = Identificacion.ToString();
                _PrimerNombre = PrimerNombre.ToString();
                _PrimeroApellido = PrimerApellido.ToString();
                _Edad = int.Parse(Edad.ToString().Trim());
                _FechaCreacion = FechaCreacion;
                _FechaModificacion = FechaModificacion;
            }
            catch (Exception ex)             
            { 
            
            }
                     
        }


        //Insertar Clientes
        public Boolean InsertClientes()
        {
            Boolean resp = false;
            int idCliente = 0;
                try
                {
                SqlConnection Cn = new SqlConnection(Cnstr);
                string sql = "InsertarClientes";
                SqlCommand cmd = new SqlCommand(sql, Cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Identificacion", _Identificacion);
                cmd.Parameters.AddWithValue("@PrimerNombre", _PrimerNombre);
                cmd.Parameters.AddWithValue("@PrimerApellido", _PrimeroApellido);
                cmd.Parameters.AddWithValue("@Edad", _Edad);

                Cn.Open();
                idCliente = int.Parse(cmd.ExecuteScalar().ToString().Trim());
                Cn.Close();
                resp = true;
                }
                catch (Exception ex)
                {
                    resp = false;
                }

                return resp;           
        }

        //Modificar Clientes
        public Boolean ActualizarClientes()
        {
            Boolean resp = false;            
                int idCliente = 0;
                try
                {
                    SqlConnection Cn = new SqlConnection(Cnstr);
                    string sql = "ActualizarClientes";
                    SqlCommand cmd = new SqlCommand(sql, Cn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdCliente", _IdCliente);
                    cmd.Parameters.AddWithValue("@Identificacion", _Identificacion);
                    cmd.Parameters.AddWithValue("@PrimerNombre", _PrimerNombre);
                    cmd.Parameters.AddWithValue("@PrimerApellido", _PrimeroApellido);
                    cmd.Parameters.AddWithValue("@Edad", _Edad);
                    Cn.Open();
                    idCliente = int.Parse(cmd.ExecuteScalar().ToString().Trim());
                    Cn.Close();
                resp = true;
                }
                catch (Exception ex)
                {
                    resp = false;
                }

                return resp;

            
        }


        //Eliminar Clientes
        public Boolean EliminarClientes()
        {
            Boolean resp = false;
            try
            {
                SqlConnection Cn = new SqlConnection(Cnstr);
                string sql = "EliminarClientes";
                SqlCommand cmd = new SqlCommand(sql, Cn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Id", _IdCliente);
                Cn.Open();
                cmd.ExecuteNonQuery();
                Cn.Close();
                resp = true;
            }
            catch (Exception ex)
            {
                resp = false;
            }

            return resp;
        }

        public Boolean GetCliente(int IdCliente)
        {
            Boolean resp = false;
            try
            {
                DataSet dsCliente = new DataSet();
                SqlConnection Cnn = new SqlConnection(Cnstr);
                string sql = "BuscarClientes";
                SqlDataAdapter da = new SqlDataAdapter(sql, Cnn);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.SelectCommand.Parameters.AddWithValue("@Id", IdCliente);
                da.Fill(dsCliente);
                if (dsCliente.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsCliente.Tables[0].Rows)
                    {
                        DataRow drw = dsCliente.Tables[0].Rows[0];
                        _IdCliente = int.Parse(dr["idCliente"].ToString().Trim());
                        _Identificacion = dr["Identificacion"].ToString().Trim();
                        _PrimerNombre = dr["PrimerNombre"].ToString().Trim();
                        _PrimeroApellido = dr["PrimerApellido"].ToString().Trim();
                        _Edad = int.Parse(dr["Edad"].ToString().Trim());
                        _FechaCreacion = DateTime.Parse(dr["FechaCreacion"].ToString().Trim());
                        if (dr["FechaModificacion"].ToString().Trim() != "")
                        {
                            _FechaModificacion = dr["FechaModificacion"].ToString().Trim();
                        }

                        resp = true;
                    }
                }
            }
            catch
            {
                resp = false;
            }
            return resp;
        }
    }
}