# Obligatorio_DB_AplicacionWeb
Obligatorio Bases de datos. Segundo semestre 2023

CONFIGURACIÓN DE LA BASE DE DATOS
La configuración se basa en 3 partes. 
  1. Archivo Dockerfile en el repositorio obligarorio_11.2023_DB el cual configura la aceptación de contenedores en Docker
  2. HomeController: Se declara la configuración/conectividad con la base en la variable connectionString.
  3. La clase sqlConnectionHelper.cs es el nexo entre la base de datos y el proyecto de backend.

LOGICA DEL PROGRAMA
La implementación puede ir por dos flujos, la del usuario Administrador registrado en la tabla "Administradores" (debe haber administradores registrados a través de la base de datos para poder acceder a todas las funcionalidades. El segundo flujo, más acotado es el de el usuario común. 

La generación de funcionarios se realiza primeramente por la generación de un registro en la tabla Logins (Id correspondiente a la CI de la persona [PK]). Este último campo mencionado corresponde también a las FK de las tablas Funcionarios, Administradores, Carnet_Salud y Agenda. Por lo tanto, sin ingreso en la tabla Logins el resto de tablas arrojará un error por falta de asociación PK y FK.

Apreciación: La tabla Agenda se registró Id en vez de Nro, ya que la agenda se solucionó por la fecha-hora y no por el número de la persona. El Id corresponde a un autonumerico en la tabla Reservas_Disponibles para el control de la misma fecha disponible ingresada.

LOGIN
Si el usuario está registrado podrá colorar su usuario (Ci) y contraseña el cual lo dejará ingresar a los formularios. En caso contrario deberá registrarse en el botón de registro, cargará sus datos y se generará un registro en Logins el cual tendrá una contraseña por defecto "nombre.apellido" (lo especifica en la registración).

SOLAPAS HABILITADAS PARA CADA USUARIO
  ADMINISTRADOR: 
    1.  Funcionarios: Lista los usuarios registrados. Se habilita la opción de "Detalle" y "Eliminación" de funcionarios.
    2.  Periodo Actualización: Formulario donde se registran los períodos habilitados para que los funcionarios puedan completar la ficha de su carné de salud. En caso de que se quiera ingresar una ficha de carné de salud fuera de este rango de fechas no se habilitará la opción.
    3.  Lista Vencidos: Lista de Funcionarios con su carné de salud vencido, para que el usuario administrador pueda realizar la gestión de contactarlos y solicitar que lo completen. Detalla datos de contacto.
    4.  Fechas Clínica: Lista las fechas disponibles que cargó el usuario administrador para habilitarla a los funcionarios (si se toma la fecha, se borra de la lista). Va directamente ligada a la tabla "Reservas_Disponibles". Hablita el botón "Agregar Fecha Disponible" para que el usuario Admin cargue nuevas. 
    5.  Mantenimiento Formulario: Formulario de carga/actualización de carné de salud para funcionarios. Carga archivos JPG, JPEG y PNG pequeños.
    6.  Reservas: Corresponde a la tabla Agenda. Habilita opcion de reservar fecha y hora para clínica según la preferencia del funcionario. Solo deja reservar una fecha por funcionario. 
    7.  Mi Agenda: Lista la agenda reservada del usuario logueado.
    8.  Cerrar Sesion:  Cierra la sesión de la persona logueada.
  
  USUARIO SIN PRIVILEGIOS
    1.  Mantenimiento Formulario: idem Administrador.
    2.  Reservas: idem Administrador.
    3.  Cerrar Sesión: idem Administrador.
    4.  Mi Agenda: idem Administrador.


