using System;
using DRS.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DRS.PostgreSQL.Models
{
    public partial class DRSContext : DbContext
    {
        public DRSContext()
        {
        }

        public DRSContext(DbContextOptions<DRSContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accessory> Accessories { get; set; }
        public virtual DbSet<Action> Actions { get; set; }
        public virtual DbSet<Actionsmenu> Actionsmenus { get; set; }
        public virtual DbSet<Carrier> Carriers { get; set; }
        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Clientaddress> Clientaddresses { get; set; }
        public virtual DbSet<Commentsreturn> Commentsreturns { get; set; }
        public virtual DbSet<Defaultserviceorder> Defaultserviceorders { get; set; }
        public virtual DbSet<Distributionchannel> Distributionchannels { get; set; }
        public virtual DbSet<Emaildestinataryconfigurationgroup> Emaildestinataryconfigurationgroups { get; set; }
        public virtual DbSet<Emaildestinatarydefault> Emaildestinatarydefaults { get; set; }
        public virtual DbSet<Emaillayout> Emaillayouts { get; set; }
        public virtual DbSet<Emaillayoutconfiguration> Emaillayoutconfigurations { get; set; }
        public virtual DbSet<Emaillayoutconfigurationgroup> Emaillayoutconfigurationgroups { get; set; }
        public virtual DbSet<Emaillog> Emaillogs { get; set; }
        public virtual DbSet<Emaillogdestinatary> Emaillogdestinataries { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Employeeclient> Employeeclients { get; set; }
        public virtual DbSet<Incident> Incidents { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Menuuserrole> Menuuserroles { get; set; }
        public virtual DbSet<Operationtype> Operationtypes { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Personemployee> Personemployees { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Productclient> Productclients { get; set; }
        public virtual DbSet<Raprofile> Raprofiles { get; set; }
        public virtual DbSet<Reasonsend> Reasonsends { get; set; }
        public virtual DbSet<Recovery> Recoveries { get; set; }
        public virtual DbSet<Return> Returns { get; set; }
        public virtual DbSet<Returnoperation> Returnoperations { get; set; }
        public virtual DbSet<Returnsdetail> Returnsdetails { get; set; }
        public virtual DbSet<Returnstatus> Returnstatuses { get; set; }
        public virtual DbSet<Statusdetail> Statusdetails { get; set; }
        public virtual DbSet<Stockconfirm> Stockconfirms { get; set; }
        public virtual DbSet<Stockconfirmation> Stockconfirmations { get; set; }
        public virtual DbSet<Stockconfirmationdetail> Stockconfirmationdetails { get; set; }
        public virtual DbSet<Stockconfirmationstatus> Stockconfirmationstatuses { get; set; }
        public virtual DbSet<Stockconfirmdetail> Stockconfirmdetails { get; set; }
        public virtual DbSet<Stockconfirmstatus> Stockconfirmstatuses { get; set; }
        public virtual DbSet<Taxresidence> Taxresidences { get; set; }
        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<Unitsale> Unitsales { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Userrole> Userroles { get; set; }
        public virtual DbSet<Userskey> Userskeys { get; set; }
        public virtual DbSet<Userstatus> Userstatuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(ConectionDB.ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "en_US.UTF8");

            modelBuilder.Entity<Accessory>(entity =>
            {
                entity.HasKey(e => e.Accessoriesid)
                    .HasName("accessories_pkey");

                entity.ToTable("accessories");

                entity.HasIndex(e => e.Accessoriesid, "accessories_accessoriesid")
                    .IsUnique();

                entity.Property(e => e.Accessoriesid).HasColumnName("accessoriesid");

                entity.Property(e => e.Accessoriescode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("accessoriescode");

                entity.Property(e => e.Accessoriesname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("accessoriesname");

                entity.Property(e => e.Accessoriesnamecomplete)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("accessoriesnamecomplete");

                entity.Property(e => e.Accessoriesstatus).HasColumnName("accessoriesstatus");

                entity.Property(e => e.Accessoriesunit)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("accessoriesunit");
            });

            modelBuilder.Entity<Action>(entity =>
            {
                entity.HasKey(e => e.Actionsid)
                    .HasName("actions_pkey");

                entity.ToTable("actions");

                entity.HasIndex(e => e.Actionsid, "actions_actionsid")
                    .IsUnique();

                entity.Property(e => e.Actionsid).HasColumnName("actionsid");

                entity.Property(e => e.Actionsdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("actionsdescription");

                entity.Property(e => e.Actionsorderid).HasColumnName("actionsorderid");

                entity.Property(e => e.Actionsparentid).HasColumnName("actionsparentid");

                entity.Property(e => e.Actionstypeid).HasColumnName("actionstypeid");
            });

            modelBuilder.Entity<Actionsmenu>(entity =>
            {
                entity.HasKey(e => new { e.Actionsid, e.Menuid })
                    .HasName("actionsmenu_pkey");

                entity.ToTable("actionsmenu");

                entity.Property(e => e.Actionsid).HasColumnName("actionsid");

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.HasOne(d => d.Actions)
                    .WithMany(p => p.Actionsmenus)
                    .HasForeignKey(d => d.Actionsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkactionsmen605684");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Actionsmenus)
                    .HasForeignKey(d => d.Menuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkactionsmen400509");
            });

            modelBuilder.Entity<Carrier>(entity =>
            {
                entity.ToTable("carrier");

                entity.HasIndex(e => e.Carriername, "carrier_carriername_key")
                    .IsUnique();

                entity.Property(e => e.Carrierid).HasColumnName("carrierid");

                entity.Property(e => e.Carriername)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("carriername");
            });

            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("client");

                entity.HasIndex(e => e.Clientid, "client_clientid")
                    .IsUnique();

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Clientcode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("clientcode");

                entity.Property(e => e.Clientname)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("clientname");

                entity.Property(e => e.Clientstatus).HasColumnName("clientstatus");
            });

            modelBuilder.Entity<Clientaddress>(entity =>
            {
                entity.ToTable("clientaddress");

                entity.HasIndex(e => e.Clientaddressid, "clientaddress_clientaddressid")
                    .IsUnique();

                entity.Property(e => e.Clientaddressid).HasColumnName("clientaddressid");

                entity.Property(e => e.Clientaddresscityname)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddresscityname");

                entity.Property(e => e.Clientaddresscountry)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddresscountry");

                entity.Property(e => e.Clientaddressdistrictname)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddressdistrictname");

                entity.Property(e => e.Clientaddressemail)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddressemail");

                entity.Property(e => e.Clientaddresslegalname)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddresslegalname");

                entity.Property(e => e.Clientaddressnumber)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddressnumber");

                entity.Property(e => e.Clientaddressphonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("clientaddressphonenumber");

                entity.Property(e => e.Clientaddresspostalcode)
                    .HasMaxLength(100)
                    .HasColumnName("clientaddresspostalcode");

                entity.Property(e => e.Clientaddressregioncode)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddressregioncode");

                entity.Property(e => e.Clientaddressstreetname)
                    .HasMaxLength(255)
                    .HasColumnName("clientaddressstreetname");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Clientaddresses)
                    .HasForeignKey(d => d.Clientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkclientaddr198336");
            });

            modelBuilder.Entity<Commentsreturn>(entity =>
            {
                entity.HasKey(e => e.Commentsreturnsid)
                    .HasName("commentsreturns_pkey");

                entity.ToTable("commentsreturns");

                entity.HasIndex(e => e.Commentsreturnsid, "commentsreturns_commentsreturnsid")
                    .IsUnique();

                entity.Property(e => e.Commentsreturnsid).HasColumnName("commentsreturnsid");

                entity.Property(e => e.Commentsreturnscreatedate).HasColumnName("commentsreturnscreatedate");

                entity.Property(e => e.Commentsreturnsdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("commentsreturnsdescription");

                entity.Property(e => e.Returnsid).HasColumnName("returnsid");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Returns)
                    .WithMany(p => p.Commentsreturns)
                    .HasForeignKey(d => d.Returnsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkcommentsre532495");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Commentsreturns)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkcommentsre810045");
            });

            modelBuilder.Entity<Defaultserviceorder>(entity =>
            {
                entity.ToTable("defaultserviceorder");

                entity.HasIndex(e => e.Defaultserviceorderid, "defaultserviceorder_defaultserviceorderid")
                    .IsUnique();

                entity.Property(e => e.Defaultserviceorderid).HasColumnName("defaultserviceorderid");

                entity.Property(e => e.Defaultserviceordername)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("defaultserviceordername");
            });

            modelBuilder.Entity<Distributionchannel>(entity =>
            {
                entity.ToTable("distributionchannel");

                entity.HasIndex(e => e.Distributionchannelid, "distributionchannel_distributionchannelid")
                    .IsUnique();

                entity.Property(e => e.Distributionchannelid).HasColumnName("distributionchannelid");

                entity.Property(e => e.Distributionchannelname)
                    .IsRequired()
                    .HasMaxLength(3)
                    .HasColumnName("distributionchannelname");
            });

            modelBuilder.Entity<Emaildestinataryconfigurationgroup>(entity =>
            {
                entity.ToTable("emaildestinataryconfigurationgroup");

                entity.HasIndex(e => e.Emaildestinataryconfigurationgroupid, "emaildestinataryconfigurationgroup_emaildestinataryconfiguratio")
                    .IsUnique();

                entity.Property(e => e.Emaildestinataryconfigurationgroupid)
                    .HasColumnName("emaildestinataryconfigurationgroupid")
                    .HasDefaultValueSql("nextval('emaildestinataryconfiguration_emaildestinataryconfiguration_seq'::regclass)");

                entity.Property(e => e.Emaildestinatarydefaultid).HasColumnName("emaildestinatarydefaultid");

                entity.Property(e => e.Emaillayoutconfigurationgroupid).HasColumnName("emaillayoutconfigurationgroupid");

                entity.HasOne(d => d.Emaildestinatarydefault)
                    .WithMany(p => p.Emaildestinataryconfigurationgroups)
                    .HasForeignKey(d => d.Emaildestinatarydefaultid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaildesti601780");

                entity.HasOne(d => d.Emaillayoutconfigurationgroup)
                    .WithMany(p => p.Emaildestinataryconfigurationgroups)
                    .HasForeignKey(d => d.Emaillayoutconfigurationgroupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaildesti622285");
            });

            modelBuilder.Entity<Emaildestinatarydefault>(entity =>
            {
                entity.ToTable("emaildestinatarydefault");

                entity.HasIndex(e => e.Emaildestinatarydefaultemail, "emaildestinatarydefault_emaildestinatarydefaultemail_key")
                    .IsUnique();

                entity.HasIndex(e => e.Emaildestinatarydefaultid, "emaildestinatarydefault_emaildestinatarydefaultid")
                    .IsUnique();

                entity.Property(e => e.Emaildestinatarydefaultid).HasColumnName("emaildestinatarydefaultid");

                entity.Property(e => e.Emaildestinatarydefaultemail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("emaildestinatarydefaultemail");
            });

            modelBuilder.Entity<Emaillayout>(entity =>
            {
                entity.ToTable("emaillayout");

                entity.HasIndex(e => e.Emaillayoutid, "emaillayout_emaillayoutid")
                    .IsUnique();

                entity.HasIndex(e => e.Emaillayoutname, "emaillayout_emaillayoutname_key")
                    .IsUnique();

                entity.Property(e => e.Emaillayoutid).HasColumnName("emaillayoutid");

                entity.Property(e => e.Emaillayoutbody)
                    .IsRequired()
                    .HasColumnName("emaillayoutbody");

                entity.Property(e => e.Emaillayoutname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("emaillayoutname");
            });

            modelBuilder.Entity<Emaillayoutconfiguration>(entity =>
            {
                entity.ToTable("emaillayoutconfiguration");

                entity.HasIndex(e => e.Emaillayoutconfigurationid, "emaillayoutconfiguration_emaillayoutconfigurationid")
                    .IsUnique();

                entity.Property(e => e.Emaillayoutconfigurationid).HasColumnName("emaillayoutconfigurationid");

                entity.Property(e => e.Emaillayoutconfigurationgroupid).HasColumnName("emaillayoutconfigurationgroupid");

                entity.Property(e => e.Emaillayoutid).HasColumnName("emaillayoutid");

                entity.HasOne(d => d.Emaillayoutconfigurationgroup)
                    .WithMany(p => p.Emaillayoutconfigurations)
                    .HasForeignKey(d => d.Emaillayoutconfigurationgroupid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaillayou641223");

                entity.HasOne(d => d.Emaillayout)
                    .WithMany(p => p.Emaillayoutconfigurations)
                    .HasForeignKey(d => d.Emaillayoutid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaillayou992453");
            });

            modelBuilder.Entity<Emaillayoutconfigurationgroup>(entity =>
            {
                entity.ToTable("emaillayoutconfigurationgroup");

                entity.HasIndex(e => e.Emaillayoutconfigurationgroupname, "emaillayoutconfigurationgroup_emaillayoutconfigurationgroup_key")
                    .IsUnique();

                entity.HasIndex(e => e.Emaillayoutconfigurationgroupid, "emaillayoutconfigurationgroup_emaillayoutconfigurationgroupid")
                    .IsUnique();

                entity.Property(e => e.Emaillayoutconfigurationgroupid)
                    .HasColumnName("emaillayoutconfigurationgroupid")
                    .HasDefaultValueSql("nextval('emaillayoutconfigurationgroup_emaillayoutconfigurationgroup_seq'::regclass)");

                entity.Property(e => e.Emaillayoutconfigurationgroupname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("emaillayoutconfigurationgroupname");
            });

            modelBuilder.Entity<Emaillog>(entity =>
            {
                entity.ToTable("emaillog");

                entity.HasIndex(e => e.Emaillogid, "emaillog_emaillogid")
                    .IsUnique();

                entity.Property(e => e.Emaillogid).HasColumnName("emaillogid");

                entity.Property(e => e.Emaillayoutid).HasColumnName("emaillayoutid");

                entity.Property(e => e.Emaillogcreatedate).HasColumnName("emaillogcreatedate");

                entity.Property(e => e.Emailloghtml)
                    .IsRequired()
                    .HasColumnName("emailloghtml");

                entity.Property(e => e.Emaillogsenddate).HasColumnName("emaillogsenddate");

                entity.Property(e => e.Emaillogstatusid).HasColumnName("emaillogstatusid");

                entity.Property(e => e.Emaillogsubject)
                    .IsRequired()
                    .HasMaxLength(500)
                    .HasColumnName("emaillogsubject");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Emaillayout)
                    .WithMany(p => p.Emaillogs)
                    .HasForeignKey(d => d.Emaillayoutid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaillog60194");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Emaillogs)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaillog90890");
            });

            modelBuilder.Entity<Emaillogdestinatary>(entity =>
            {
                entity.ToTable("emaillogdestinatary");

                entity.HasIndex(e => e.Emaillogdestinataryid, "emaillogdestinatary_emaillogdestinataryid")
                    .IsUnique();

                entity.Property(e => e.Emaillogdestinataryid).HasColumnName("emaillogdestinataryid");

                entity.Property(e => e.Emaillogdestinataryemail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("emaillogdestinataryemail");

                entity.Property(e => e.Emaillogdestinatarytype).HasColumnName("emaillogdestinatarytype");

                entity.Property(e => e.Emaillogid).HasColumnName("emaillogid");

                entity.HasOne(d => d.Emaillog)
                    .WithMany(p => p.Emaillogdestinataries)
                    .HasForeignKey(d => d.Emaillogid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemaillogde300146");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employee");

                entity.HasIndex(e => e.CbusinessPartnerId, "employee_cbusiness_partner_id_key")
                    .IsUnique();

                entity.HasIndex(e => e.CeeUuid, "employee_cee_uuid_key")
                    .IsUnique();

                entity.HasIndex(e => e.Employeeid, "employee_employeeid")
                    .IsUnique();

                entity.HasIndex(e => e.TeeId, "employee_tee_id_key")
                    .IsUnique();

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.CbusinessPartnerId)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("cbusiness_partner_id");

                entity.Property(e => e.CeeUuid)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("cee_uuid");

                entity.Property(e => e.TeeId)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("tee_id");

                entity.Property(e => e.TypeEmployee)
                    .IsRequired()
                    .HasMaxLength(5)
                    .HasColumnName("type_employee");
            });

            modelBuilder.Entity<Employeeclient>(entity =>
            {
                entity.HasKey(e => new { e.Employeeid, e.Clientid })
                    .HasName("employeeclient_pkey");

                entity.ToTable("employeeclient");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Employeeclients)
                    .HasForeignKey(d => d.Clientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemployeecl515298");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Employeeclients)
                    .HasForeignKey(d => d.Employeeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkemployeecl369492");
            });

            modelBuilder.Entity<Incident>(entity =>
            {
                entity.HasKey(e => e.Incidentsid)
                    .HasName("incidents_pkey");

                entity.ToTable("incidents");

                entity.HasIndex(e => e.Incidentsid, "incidents_incidentsid")
                    .IsUnique();

                entity.Property(e => e.Incidentsid).HasColumnName("incidentsid");

                entity.Property(e => e.Incidentscreatedate).HasColumnName("incidentscreatedate");

                entity.Property(e => e.Incidentsdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("incidentsdescription");

                entity.Property(e => e.Usersid).HasColumnName("usersid");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("location");

                entity.HasIndex(e => e.Locationid, "location_locationid")
                    .IsUnique();

                entity.Property(e => e.Locationid).HasColumnName("locationid");

                entity.Property(e => e.Locationname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("locationname");

                entity.Property(e => e.Locationtype)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("locationtype");
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.ToTable("menu");

                entity.HasIndex(e => e.Menuid, "menu_menuid")
                    .IsUnique();

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Menuaction)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("menuaction");

                entity.Property(e => e.Menucontroller)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("menucontroller");

                entity.Property(e => e.Menucreatedate)
                    .HasColumnType("date")
                    .HasColumnName("menucreatedate");

                entity.Property(e => e.Menuenabled).HasColumnName("menuenabled");

                entity.Property(e => e.Menuicon)
                    .HasMaxLength(255)
                    .HasColumnName("menuicon");

                entity.Property(e => e.Menuislinked).HasColumnName("menuislinked");

                entity.Property(e => e.Menuname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("menuname");

                entity.Property(e => e.Menuorder).HasColumnName("menuorder");

                entity.Property(e => e.Menuparentid).HasColumnName("menuparentid");
            });

            modelBuilder.Entity<Menuuserrole>(entity =>
            {
                entity.HasKey(e => new { e.Menuid, e.Userroleid })
                    .HasName("menuuserrole_pkey");

                entity.ToTable("menuuserrole");

                entity.Property(e => e.Menuid).HasColumnName("menuid");

                entity.Property(e => e.Userroleid).HasColumnName("userroleid");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.Menuuserroles)
                    .HasForeignKey(d => d.Menuid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkmenuuserro689331");

                entity.HasOne(d => d.Userrole)
                    .WithMany(p => p.Menuuserroles)
                    .HasForeignKey(d => d.Userroleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkmenuuserro129932");
            });

            modelBuilder.Entity<Operationtype>(entity =>
            {
                entity.ToTable("operationtype");

                entity.HasIndex(e => e.Operationtypeid, "operationtype_operationtypeid")
                    .IsUnique();

                entity.Property(e => e.Operationtypeid).HasColumnName("operationtypeid");

                entity.Property(e => e.Operationtypename)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("operationtypename");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(e => new { e.Userroleid, e.Actionsid })
                    .HasName("permission_pkey");

                entity.ToTable("permission");

                entity.Property(e => e.Userroleid).HasColumnName("userroleid");

                entity.Property(e => e.Actionsid).HasColumnName("actionsid");

                entity.HasOne(d => d.Actions)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.Actionsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkpermission294564");

                entity.HasOne(d => d.Userrole)
                    .WithMany(p => p.Permissions)
                    .HasForeignKey(d => d.Userroleid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkpermission652587");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.HasIndex(e => e.Personid, "person_personid")
                    .IsUnique();

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.Property(e => e.Persongender)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("persongender");

                entity.Property(e => e.Personisemployee).HasColumnName("personisemployee");

                entity.Property(e => e.Personlastname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("personlastname");

                entity.Property(e => e.Personname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("personname");
            });

            modelBuilder.Entity<Personemployee>(entity =>
            {
                entity.HasKey(e => new { e.Personid, e.Employeeid })
                    .HasName("personemployee_pkey");

                entity.ToTable("personemployee");

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Personemployees)
                    .HasForeignKey(d => d.Employeeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkpersonempl234762");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Personemployees)
                    .HasForeignKey(d => d.Personid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkpersonempl144515");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("plan");

                entity.HasIndex(e => e.Planname, "Plan_planname_key")
                    .IsUnique();

                entity.Property(e => e.Planid)
                    .HasColumnName("planid")
                    .HasDefaultValueSql("nextval('\"Plan_planid_seq\"'::regclass)");

                entity.Property(e => e.Planname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("planname");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("products");

                entity.HasIndex(e => e.Productid, "products_productid")
                    .IsUnique();

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Productcode)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("productcode");

                entity.Property(e => e.Productname)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasColumnName("productname");

                entity.Property(e => e.Producttechnology)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("producttechnology");
            });

            modelBuilder.Entity<Productclient>(entity =>
            {
                entity.ToTable("productclient");

                entity.HasIndex(e => e.Productclientid, "productclient_productclientid")
                    .IsUnique();

                entity.Property(e => e.Productclientid).HasColumnName("productclientid");

                entity.Property(e => e.Carrierid).HasColumnName("carrierid");

                entity.Property(e => e.Carriername)
                    .HasMaxLength(200)
                    .HasColumnName("carriername");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Consoleactive)
                    .HasMaxLength(200)
                    .HasColumnName("consoleactive");

                entity.Property(e => e.Imsi)
                    .HasMaxLength(255)
                    .HasColumnName("imsi");

                entity.Property(e => e.Number)
                    .HasMaxLength(255)
                    .HasColumnName("number");

                entity.Property(e => e.Planid).HasColumnName("planid");

                entity.Property(e => e.Planname)
                    .HasMaxLength(150)
                    .HasColumnName("planname");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Serie)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("serie");

                entity.Property(e => e.Sim)
                    .HasMaxLength(255)
                    .HasColumnName("sim");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Productclients)
                    .HasForeignKey(d => d.Clientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkproductcli856137");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Productclients)
                    .HasForeignKey(d => d.Productid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkproductcli216527");
            });

            modelBuilder.Entity<Raprofile>(entity =>
            {
                entity.ToTable("raprofile");

                entity.HasIndex(e => e.Raprofileid, "raprofile_raprofileid")
                    .IsUnique();

                entity.Property(e => e.Raprofileid).HasColumnName("raprofileid");

                entity.Property(e => e.Defaultserviceorderid).HasColumnName("defaultserviceorderid");

                entity.Property(e => e.Distributionchannelid).HasColumnName("distributionchannelid");

                entity.Property(e => e.Locationidavailable).HasColumnName("locationidavailable");

                entity.Property(e => e.Locationidquality).HasColumnName("locationidquality");

                entity.Property(e => e.Locationidrepair).HasColumnName("locationidrepair");

                entity.Property(e => e.Locationidresidence).HasColumnName("locationidresidence");

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.Property(e => e.Taxresidenceid).HasColumnName("taxresidenceid");

                entity.Property(e => e.Unitsalesid).HasColumnName("unitsalesid");

                entity.HasOne(d => d.Defaultserviceorder)
                    .WithMany(p => p.Raprofiles)
                    .HasForeignKey(d => d.Defaultserviceorderid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile701993");

                entity.HasOne(d => d.Distributionchannel)
                    .WithMany(p => p.Raprofiles)
                    .HasForeignKey(d => d.Distributionchannelid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile172664");

                entity.HasOne(d => d.LocationidavailableNavigation)
                    .WithMany(p => p.RaprofileLocationidavailableNavigations)
                    .HasForeignKey(d => d.Locationidavailable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile684349");

                entity.HasOne(d => d.LocationidqualityNavigation)
                    .WithMany(p => p.RaprofileLocationidqualityNavigations)
                    .HasForeignKey(d => d.Locationidquality)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile705104");

                entity.HasOne(d => d.LocationidrepairNavigation)
                    .WithMany(p => p.RaprofileLocationidrepairNavigations)
                    .HasForeignKey(d => d.Locationidrepair)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile238250");

                entity.HasOne(d => d.LocationidresidenceNavigation)
                    .WithMany(p => p.RaprofileLocationidresidenceNavigations)
                    .HasForeignKey(d => d.Locationidresidence)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile265705");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Raprofiles)
                    .HasForeignKey(d => d.Personid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile862554");

                entity.HasOne(d => d.Taxresidence)
                    .WithMany(p => p.Raprofiles)
                    .HasForeignKey(d => d.Taxresidenceid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile480004");

                entity.HasOne(d => d.Unitsales)
                    .WithMany(p => p.Raprofiles)
                    .HasForeignKey(d => d.Unitsalesid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkraprofile617684");
            });

            modelBuilder.Entity<Reasonsend>(entity =>
            {
                entity.ToTable("reasonsend");

                entity.HasIndex(e => e.Reasonsendid, "reasonsend_reasonsendid")
                    .IsUnique();

                entity.HasIndex(e => e.Reasonsendname, "reasonsend_reasonsendname_key")
                    .IsUnique();

                entity.Property(e => e.Reasonsendid).HasColumnName("reasonsendid");

                entity.Property(e => e.Reasonsendname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("reasonsendname");
            });

            modelBuilder.Entity<Recovery>(entity =>
            {
                entity.ToTable("recovery");

                entity.HasIndex(e => e.Recoveryid, "recovery_recoveryid")
                    .IsUnique();

                entity.Property(e => e.Recoveryid).HasColumnName("recoveryid");

                entity.Property(e => e.Recoverycreatedate).HasColumnName("recoverycreatedate");

                entity.Property(e => e.Recoverykey)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("recoverykey");

                entity.Property(e => e.Recoverystatus).HasColumnName("recoverystatus");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Recoveries)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkrecovery335253");
            });

            modelBuilder.Entity<Return>(entity =>
            {
                entity.HasKey(e => e.Returnsid)
                    .HasName("returns_pkey");

                entity.ToTable("returns");

                entity.HasIndex(e => e.Returnsid, "returns_returnsid")
                    .IsUnique();

                entity.Property(e => e.Returnsid).HasColumnName("returnsid");

                entity.Property(e => e.Clientid).HasColumnName("clientid");

                entity.Property(e => e.Employeeid).HasColumnName("employeeid");

                entity.Property(e => e.Reasonsendid).HasColumnName("reasonsendid");

                entity.Property(e => e.Returnscc)
                    .HasMaxLength(150)
                    .HasColumnName("returnscc");

                entity.Property(e => e.Returnscreatedate)
                    .HasColumnType("date")
                    .HasColumnName("returnscreatedate");

                entity.Property(e => e.Returnsdescription)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdescription");

                entity.Property(e => e.Returnsguidenumber)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("returnsguidenumber");

                entity.Property(e => e.Returnsmessagername)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("returnsmessagername");

                entity.Property(e => e.Returnsquoterepair).HasColumnName("returnsquoterepair");

                entity.Property(e => e.Returnssendedfor)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("returnssendedfor");

                entity.Property(e => e.Returnstatusid).HasColumnName("returnstatusid");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.Clientid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturns580265");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.Employeeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturns695474");

                entity.HasOne(d => d.Reasonsend)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.Reasonsendid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturns648892");

                entity.HasOne(d => d.Returnstatus)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.Returnstatusid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturns177571");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Returns)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturns266562");
            });

            modelBuilder.Entity<Returnoperation>(entity =>
            {
                entity.HasKey(e => e.Returnoperationsid)
                    .HasName("returnoperations_pkey");

                entity.ToTable("returnoperations");

                entity.HasIndex(e => e.Returnoperationsid, "returnoperations_returnoperationsid")
                    .IsUnique();

                entity.Property(e => e.Returnoperationsid).HasColumnName("returnoperationsid");

                entity.Property(e => e.Returnid).HasColumnName("returnid");

                entity.Property(e => e.Returnoperationsdate)
                    .HasColumnType("date")
                    .HasColumnName("returnoperationsdate");

                entity.Property(e => e.Returnoperationsdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("returnoperationsdescription");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.Returnoperations)
                    .HasForeignKey(d => d.Returnid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnoper718349");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Returnoperations)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnoper701132");
            });

            modelBuilder.Entity<Returnsdetail>(entity =>
            {
                entity.HasKey(e => e.Returnsdetailsid)
                    .HasName("returnsdetails_pkey");

                entity.ToTable("returnsdetails");

                entity.HasIndex(e => e.Returnsdetailsid, "returnsdetails_returnsdetailsid")
                    .IsUnique();

                entity.Property(e => e.Returnsdetailsid)
                    .ValueGeneratedNever()
                    .HasColumnName("returnsdetailsid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Productname)
                    .HasMaxLength(250)
                    .HasColumnName("productname");

                entity.Property(e => e.Productquantity).HasColumnName("productquantity");

                entity.Property(e => e.Productserialnumber)
                    .HasMaxLength(100)
                    .HasColumnName("productserialnumber");

                entity.Property(e => e.Returnsdetailsalias)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsalias");

                entity.Property(e => e.Returnsdetailsantenna).HasColumnName("returnsdetailsantenna");

                entity.Property(e => e.Returnsdetailsbattery).HasColumnName("returnsdetailsbattery");

                entity.Property(e => e.Returnsdetailsbox).HasColumnName("returnsdetailsbox");

                entity.Property(e => e.Returnsdetailscalledidentifier).HasColumnName("returnsdetailscalledidentifier");

                entity.Property(e => e.Returnsdetailscallprivate).HasColumnName("returnsdetailscallprivate");

                entity.Property(e => e.Returnsdetailscarrierid)
                    .HasColumnName("returnsdetailscarrierid")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Returnsdetailscase).HasColumnName("returnsdetailscase");

                entity.Property(e => e.Returnsdetailscharged).HasColumnName("returnsdetailscharged");

                entity.Property(e => e.Returnsdetailschargedbase).HasColumnName("returnsdetailschargedbase");

                entity.Property(e => e.Returnsdetailsclip).HasColumnName("returnsdetailsclip");

                entity.Property(e => e.Returnsdetailscover).HasColumnName("returnsdetailscover");

                entity.Property(e => e.Returnsdetailsdialnumber)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsdialnumber");

                entity.Property(e => e.Returnsdetailsdisplay).HasColumnName("returnsdetailsdisplay");

                entity.Property(e => e.Returnsdetailsevidenceforms).HasColumnName("returnsdetailsevidenceforms");

                entity.Property(e => e.Returnsdetailsevidencelite).HasColumnName("returnsdetailsevidencelite");

                entity.Property(e => e.Returnsdetailsextractiontool).HasColumnName("returnsdetailsextractiontool");

                entity.Property(e => e.Returnsdetailsfoldername)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsfoldername");

                entity.Property(e => e.Returnsdetailsgps).HasColumnName("returnsdetailsgps");

                entity.Property(e => e.Returnsdetailsgroup)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsgroup");

                entity.Property(e => e.Returnsdetailsgroups)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsgroups");

                entity.Property(e => e.Returnsdetailskeyboard).HasColumnName("returnsdetailskeyboard");

                entity.Property(e => e.Returnsdetailslegend)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailslegend");

                entity.Property(e => e.Returnsdetailsmanual).HasColumnName("returnsdetailsmanual");

                entity.Property(e => e.Returnsdetailsobservations)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsobservations");

                entity.Property(e => e.Returnsdetailsother)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailsother");

                entity.Property(e => e.Returnsdetailsplanid)
                    .HasColumnName("returnsdetailsplanid")
                    .HasDefaultValueSql("0");

                entity.Property(e => e.Returnsdetailssimisservitron).HasColumnName("returnsdetailssimisservitron");

                entity.Property(e => e.Returnsdetailssuscribesites)
                    .HasMaxLength(255)
                    .HasColumnName("returnsdetailssuscribesites");

                entity.Property(e => e.Returnsdetailsteamvoxdispatch).HasColumnName("returnsdetailsteamvoxdispatch");

                entity.Property(e => e.Returnsdetailsteamvoxliteopen).HasColumnName("returnsdetailsteamvoxliteopen");

                entity.Property(e => e.Returnsdetailsteamvoxsecuremode).HasColumnName("returnsdetailsteamvoxsecuremode");

                entity.Property(e => e.Returnsdetailsusb).HasColumnName("returnsdetailsusb");

                entity.Property(e => e.Returnsdetailsusbcable).HasColumnName("returnsdetailsusbcable");

                entity.Property(e => e.Returnsdetailsusbconector).HasColumnName("returnsdetailsusbconector");

                entity.Property(e => e.Returnsdetailsusbmagneticcable).HasColumnName("returnsdetailsusbmagneticcable");

                entity.Property(e => e.Returnsdetailszaypher).HasColumnName("returnsdetailszaypher");

                entity.Property(e => e.Returnsid).HasColumnName("returnsid");

                entity.Property(e => e.Returnsisonlyaccesory).HasColumnName("returnsisonlyaccesory");

                entity.Property(e => e.Returnsisservitron).HasColumnName("returnsisservitron");

                entity.Property(e => e.Returnssim)
                    .HasMaxLength(50)
                    .HasColumnName("returnssim");

                entity.HasOne(d => d.ReturnsdetailsantennaNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsantennaNavigations)
                    .HasForeignKey(d => d.Returnsdetailsantenna)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet600105");

                entity.HasOne(d => d.ReturnsdetailsbatteryNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsbatteryNavigations)
                    .HasForeignKey(d => d.Returnsdetailsbattery)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet607887");

                entity.HasOne(d => d.ReturnsdetailsboxNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsboxNavigations)
                    .HasForeignKey(d => d.Returnsdetailsbox)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet91129");

                entity.HasOne(d => d.ReturnsdetailscaseNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailscaseNavigations)
                    .HasForeignKey(d => d.Returnsdetailscase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet624002");

                entity.HasOne(d => d.ReturnsdetailschargedNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailschargedNavigations)
                    .HasForeignKey(d => d.Returnsdetailscharged)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet304062");

                entity.HasOne(d => d.ReturnsdetailschargedbaseNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailschargedbaseNavigations)
                    .HasForeignKey(d => d.Returnsdetailschargedbase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet452349");

                entity.HasOne(d => d.ReturnsdetailsclipNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsclipNavigations)
                    .HasForeignKey(d => d.Returnsdetailsclip)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet613730");

                entity.HasOne(d => d.ReturnsdetailscoverNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailscoverNavigations)
                    .HasForeignKey(d => d.Returnsdetailscover)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet654066");

                entity.HasOne(d => d.ReturnsdetailsdisplayNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsdisplayNavigations)
                    .HasForeignKey(d => d.Returnsdetailsdisplay)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet601799");

                entity.HasOne(d => d.ReturnsdetailsextractiontoolNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsextractiontoolNavigations)
                    .HasForeignKey(d => d.Returnsdetailsextractiontool)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet796589");

                entity.HasOne(d => d.ReturnsdetailskeyboardNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailskeyboardNavigations)
                    .HasForeignKey(d => d.Returnsdetailskeyboard)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet415229");

                entity.HasOne(d => d.ReturnsdetailsmanualNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsmanualNavigations)
                    .HasForeignKey(d => d.Returnsdetailsmanual)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet246631");

                entity.HasOne(d => d.ReturnsdetailsusbNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsusbNavigations)
                    .HasForeignKey(d => d.Returnsdetailsusb)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet73792");

                entity.HasOne(d => d.ReturnsdetailsusbcableNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsusbcableNavigations)
                    .HasForeignKey(d => d.Returnsdetailsusbcable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet944548");

                entity.HasOne(d => d.ReturnsdetailsusbconectorNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsusbconectorNavigations)
                    .HasForeignKey(d => d.Returnsdetailsusbconector)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet371440");

                entity.HasOne(d => d.ReturnsdetailsusbmagneticcableNavigation)
                    .WithMany(p => p.ReturnsdetailReturnsdetailsusbmagneticcableNavigations)
                    .HasForeignKey(d => d.Returnsdetailsusbmagneticcable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet557576");

                entity.HasOne(d => d.Returns)
                    .WithMany(p => p.Returnsdetails)
                    .HasForeignKey(d => d.Returnsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkreturnsdet413208");
            });

            modelBuilder.Entity<Returnstatus>(entity =>
            {
                entity.ToTable("returnstatus");

                entity.HasIndex(e => e.Returnstatusid, "returnstatus_returnstatusid")
                    .IsUnique();

                entity.HasIndex(e => e.Returnstatusname, "returnstatus_returnstatusname_key")
                    .IsUnique();

                entity.Property(e => e.Returnstatusid).HasColumnName("returnstatusid");

                entity.Property(e => e.Returnstatusname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("returnstatusname");
            });

            modelBuilder.Entity<Statusdetail>(entity =>
            {
                entity.HasKey(e => e.Statusdetailsid)
                    .HasName("statusdetails_pkey");

                entity.ToTable("statusdetails");

                entity.HasIndex(e => e.Statusdetailsid, "statusdetails_statusdetailsid")
                    .IsUnique();

                entity.Property(e => e.Statusdetailsid).HasColumnName("statusdetailsid");

                entity.Property(e => e.Statusdetailsname)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("statusdetailsname");
            });

            modelBuilder.Entity<Stockconfirm>(entity =>
            {
                entity.ToTable("stockconfirm");

                entity.HasIndex(e => e.Stockconfirmid, "stockconfirm_stockconfirmid")
                    .IsUnique();

                entity.Property(e => e.Stockconfirmid).HasColumnName("stockconfirmid");

                entity.Property(e => e.Returnid).HasColumnName("returnid");

                entity.Property(e => e.Stockconfirmcreatedate).HasColumnName("stockconfirmcreatedate");

                entity.Property(e => e.Stockconfirmsapcode)
                    .HasMaxLength(255)
                    .HasColumnName("stockconfirmsapcode");

                entity.Property(e => e.Stockconfirmsapuuid)
                    .HasMaxLength(255)
                    .HasColumnName("stockconfirmsapuuid");

                entity.Property(e => e.Stockconfirmstatusid).HasColumnName("stockconfirmstatusid");

                entity.HasOne(d => d.Return)
                    .WithMany(p => p.Stockconfirms)
                    .HasForeignKey(d => d.Returnid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi509799");

                entity.HasOne(d => d.Stockconfirmstatus)
                    .WithMany(p => p.Stockconfirms)
                    .HasForeignKey(d => d.Stockconfirmstatusid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi159077");
            });

            modelBuilder.Entity<Stockconfirmation>(entity =>
            {
                entity.ToTable("stockconfirmation");

                entity.HasIndex(e => e.Stockconfirmationid, "stockconfirmation_stockconfirmationid")
                    .IsUnique();

                entity.Property(e => e.Stockconfirmationid).HasColumnName("stockconfirmationid");

                entity.Property(e => e.Returnsdetailsid).HasColumnName("returnsdetailsid");

                entity.Property(e => e.Stockconfirmationstatusid).HasColumnName("stockconfirmationstatusid");

                entity.HasOne(d => d.Returnsdetails)
                    .WithMany(p => p.Stockconfirmations)
                    .HasForeignKey(d => d.Returnsdetailsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi112478");

                entity.HasOne(d => d.Stockconfirmationstatus)
                    .WithMany(p => p.Stockconfirmations)
                    .HasForeignKey(d => d.Stockconfirmationstatusid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi721880");
            });

            modelBuilder.Entity<Stockconfirmationdetail>(entity =>
            {
                entity.HasKey(e => e.Stockconfirmationdetailsid)
                    .HasName("stockconfirmationdetails_pkey");

                entity.ToTable("stockconfirmationdetails");

                entity.HasIndex(e => e.Stockconfirmationdetailsid, "stockconfirmationdetails_stockconfirmationdetailsid")
                    .IsUnique();

                entity.Property(e => e.Stockconfirmationdetailsid).HasColumnName("stockconfirmationdetailsid");

                entity.Property(e => e.Stockconfirmationcomments)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("stockconfirmationcomments");

                entity.Property(e => e.Stockconfirmationcreatedate).HasColumnName("stockconfirmationcreatedate");

                entity.Property(e => e.Stockconfirmationid).HasColumnName("stockconfirmationid");

                entity.Property(e => e.Stockconfirmationquantity).HasColumnName("stockconfirmationquantity");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Stockconfirmation)
                    .WithMany(p => p.Stockconfirmationdetails)
                    .HasForeignKey(d => d.Stockconfirmationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi829593");
            });

            modelBuilder.Entity<Stockconfirmationstatus>(entity =>
            {
                entity.ToTable("stockconfirmationstatus");

                entity.HasIndex(e => e.Stockconfirmationstatusid, "stockconfirmationstatus_stockconfirmationstatusid")
                    .IsUnique();

                entity.Property(e => e.Stockconfirmationstatusid).HasColumnName("stockconfirmationstatusid");

                entity.Property(e => e.Stockconfirmationstatusname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("stockconfirmationstatusname");
            });

            modelBuilder.Entity<Stockconfirmdetail>(entity =>
            {
                entity.HasKey(e => e.Stockconfirmdetailsid)
                    .HasName("stockconfirmdetails_pkey");

                entity.ToTable("stockconfirmdetails");

                entity.HasIndex(e => e.Stockconfirmdetailsid, "stockconfirmdetails_stockconfirmdetailsid")
                    .IsUnique();

                entity.Property(e => e.Stockconfirmdetailsid).HasColumnName("stockconfirmdetailsid");

                entity.Property(e => e.Productid).HasColumnName("productid");

                entity.Property(e => e.Returndetailsid).HasColumnName("returndetailsid");

                entity.Property(e => e.Stockconfirmationdetailsid).HasColumnName("stockconfirmationdetailsid");

                entity.Property(e => e.Stockconfirmationid).HasColumnName("stockconfirmationid");

                entity.Property(e => e.Stockconfirmid).HasColumnName("stockconfirmid");

                entity.Property(e => e.Stockconfirmorderid).HasColumnName("stockconfirmorderid");

                entity.HasOne(d => d.Returndetails)
                    .WithMany(p => p.Stockconfirmdetails)
                    .HasForeignKey(d => d.Returndetailsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi386909");

                entity.HasOne(d => d.Stockconfirmationdetails)
                    .WithMany(p => p.Stockconfirmdetails)
                    .HasForeignKey(d => d.Stockconfirmationdetailsid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi239929");

                entity.HasOne(d => d.Stockconfirmation)
                    .WithMany(p => p.Stockconfirmdetails)
                    .HasForeignKey(d => d.Stockconfirmationid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi737821");

                entity.HasOne(d => d.Stockconfirm)
                    .WithMany(p => p.Stockconfirmdetails)
                    .HasForeignKey(d => d.Stockconfirmid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkstockconfi405846");
            });

            modelBuilder.Entity<Stockconfirmstatus>(entity =>
            {
                entity.ToTable("stockconfirmstatus");

                entity.Property(e => e.Stockconfirmstatusid).HasColumnName("stockconfirmstatusid");

                entity.Property(e => e.Stockconfirmstatusname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("stockconfirmstatusname");
            });

            modelBuilder.Entity<Taxresidence>(entity =>
            {
                entity.ToTable("taxresidence");

                entity.HasIndex(e => e.Taxresidenceid, "taxresidence_taxresidenceid")
                    .IsUnique();

                entity.Property(e => e.Taxresidenceid).HasColumnName("taxresidenceid");

                entity.Property(e => e.Taxresidencename)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("taxresidencename");
            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.Transactionsid)
                    .HasName("transactions_pkey");

                entity.ToTable("transactions");

                entity.HasIndex(e => e.Transactionsid, "transactions_transactionsid")
                    .IsUnique();

                entity.Property(e => e.Transactionsid).HasColumnName("transactionsid");

                entity.Property(e => e.Operationtypeid).HasColumnName("operationtypeid");

                entity.Property(e => e.Transactionscreatedate).HasColumnName("transactionscreatedate");

                entity.Property(e => e.Transactionsdescription)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("transactionsdescription");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.HasOne(d => d.Operationtype)
                    .WithMany(p => p.Transactions)
                    .HasForeignKey(d => d.Operationtypeid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fktransactio856478");
            });

            modelBuilder.Entity<Unitsale>(entity =>
            {
                entity.HasKey(e => e.Unitsalesid)
                    .HasName("unitsales_pkey");

                entity.ToTable("unitsales");

                entity.HasIndex(e => e.Unitsalesid, "unitsales_unitsalesid")
                    .IsUnique();

                entity.Property(e => e.Unitsalesid).HasColumnName("unitsalesid");

                entity.Property(e => e.Unitsalename)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("unitsalename");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Usersid)
                    .HasName("users_pkey");

                entity.ToTable("users");

                entity.HasIndex(e => e.Usersid, "users_usersid")
                    .IsUnique();

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.Property(e => e.Personid).HasColumnName("personid");

                entity.Property(e => e.Userrole).HasColumnName("userrole");

                entity.Property(e => e.Usersemail)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("usersemail");

                entity.Property(e => e.Usersphonenumber)
                    .HasMaxLength(50)
                    .HasColumnName("usersphonenumber");

                entity.Property(e => e.Userstatusid).HasColumnName("userstatusid");

                entity.HasOne(d => d.Person)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Personid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkusers122530");

                entity.HasOne(d => d.UserroleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Userrole)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkusers559682");

                entity.HasOne(d => d.Userstatus)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Userstatusid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkusers630740");
            });

            modelBuilder.Entity<Userrole>(entity =>
            {
                entity.ToTable("userrole");

                entity.HasIndex(e => e.Userroleid, "userrole_userroleid")
                    .IsUnique();

                entity.HasIndex(e => e.Userrolename, "userrole_userrolename_key")
                    .IsUnique();

                entity.Property(e => e.Userroleid).HasColumnName("userroleid");

                entity.Property(e => e.Userrolename)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("userrolename");
            });

            modelBuilder.Entity<Userskey>(entity =>
            {
                entity.HasKey(e => e.Userskeysid)
                    .HasName("userskeys_pkey");

                entity.ToTable("userskeys");

                entity.HasIndex(e => e.Userskeysid, "userskeys_userskeysid")
                    .IsUnique();

                entity.Property(e => e.Userskeysid).HasColumnName("userskeysid");

                entity.Property(e => e.Usersid).HasColumnName("usersid");

                entity.Property(e => e.Userskeyscreatedate)
                    .HasColumnType("date")
                    .HasColumnName("userskeyscreatedate");

                entity.Property(e => e.Userskeysenabled).HasColumnName("userskeysenabled");

                entity.Property(e => e.Userskeyspassword)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("userskeyspassword");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Userskeys)
                    .HasForeignKey(d => d.Usersid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkuserskeys877896");
            });

            modelBuilder.Entity<Userstatus>(entity =>
            {
                entity.ToTable("userstatus");

                entity.HasIndex(e => e.Userstatusid, "userstatus_userstatusid")
                    .IsUnique();

                entity.Property(e => e.Userstatusid).HasColumnName("userstatusid");

                entity.Property(e => e.Userstatusname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("userstatusname");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
