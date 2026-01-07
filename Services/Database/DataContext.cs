using DatabaseTask.Models.Database;
using Microsoft.EntityFrameworkCore;

namespace DatabaseTask.Services.Database
{
    public partial class DataContext : DbContext
    {
        private readonly string _connectionString;

        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public virtual DbSet<DbMigtation> DbMigtations { get; set; } = null!;
        public virtual DbSet<DbversionTable> DbversionTables { get; set; } = null!;
        public virtual DbSet<DeviceParameterSet> DeviceParameterSets { get; set; } = null!;
        public virtual DbSet<DwgByType> DwgByTypes { get; set; } = null!;
        public virtual DbSet<GetCategoryDatum> GetCategoryData { get; set; } = null!;
        public virtual DbSet<GetJoinCategoryAndDevice> GetJoinCategoryAndDevices { get; set; } = null!;
        public virtual DbSet<NotSuitParameter> NotSuitParameters { get; set; } = null!;
        public virtual DbSet<SelectFieldDatum> SelectFieldData { get; set; } = null!;
        public virtual DbSet<SuitParameter> SuitParameters { get; set; } = null!;
        public virtual DbSet<TblAccessoryMatrix> TblAccessoryMatrices { get; set; } = null!;
        public virtual DbSet<TblArray> TblArrays { get; set; } = null!;
        public virtual DbSet<TblBusVertex> TblBusVertexes { get; set; } = null!;
        public virtual DbSet<TblBuse> TblBuses { get; set; } = null!;
        public virtual DbSet<TblCabinet> TblCabinets { get; set; } = null!;
        public virtual DbSet<TblCategory> TblCategories { get; set; } = null!;
        public virtual DbSet<TblChangedItem> TblChangedItems { get; set; } = null!;
        public virtual DbSet<TblChangedSqlHistory> TblChangedSqlHistories { get; set; } = null!;
        public virtual DbSet<TblCluster> TblClusters { get; set; } = null!;
        public virtual DbSet<TblDbDevice> TblDbDevices { get; set; } = null!;
        public virtual DbSet<TblDbManager> TblDbManagers { get; set; } = null!;
        public virtual DbSet<TblDbversionPrice> TblDbversionPrices { get; set; } = null!;
        public virtual DbSet<TblDecoration> TblDecorations { get; set; } = null!;
        public virtual DbSet<TblDevice> TblDevices { get; set; } = null!;
        public virtual DbSet<TblDeviceAccessory> TblDeviceAccessories { get; set; } = null!;
        public virtual DbSet<TblDeviceCategoryParameter> TblDeviceCategoryParameters { get; set; } = null!;
        public virtual DbSet<TblDeviceParameter> TblDeviceParameters { get; set; } = null!;
        public virtual DbSet<TblDeviceValue> TblDeviceValues { get; set; } = null!;
        public virtual DbSet<TblDirectorySystem> TblDirectorySystems { get; set; } = null!;
        public virtual DbSet<TblDrawingAssembly> TblDrawingAssemblies { get; set; } = null!;
        public virtual DbSet<TblDrawingAttachment> TblDrawingAttachments { get; set; } = null!;
        public virtual DbSet<TblDrawingConductor> TblDrawingConductors { get; set; } = null!;
        public virtual DbSet<TblDrawingConductorWire> TblDrawingConductorWires { get; set; } = null!;
        public virtual DbSet<TblDrawingConnector> TblDrawingConnectors { get; set; } = null!;
        public virtual DbSet<TblDrawingContainer> TblDrawingContainers { get; set; } = null!;
        public virtual DbSet<TblDrawingContent> TblDrawingContents { get; set; } = null!;
        public virtual DbSet<TblDrawingJoint> TblDrawingJoints { get; set; } = null!;
        public virtual DbSet<TblDrawingJointRelation> TblDrawingJointRelations { get; set; } = null!;
        public virtual DbSet<TblDrawingRelation> TblDrawingRelations { get; set; } = null!;
        public virtual DbSet<TblDrawingSolution> TblDrawingSolutions { get; set; } = null!;
        public virtual DbSet<TblDrawingTrace> TblDrawingTraces { get; set; } = null!;
        public virtual DbSet<TblDrawingTraceVertex> TblDrawingTraceVertexes { get; set; } = null!;
        public virtual DbSet<TblDrawingUnit> TblDrawingUnits { get; set; } = null!;
        public virtual DbSet<TblElement> TblElements { get; set; } = null!;
        public virtual DbSet<TblElementComponent> TblElementComponents { get; set; } = null!;
        public virtual DbSet<TblElementComponentInstance> TblElementComponentInstances { get; set; } = null!;
        public virtual DbSet<TblElementConnector> TblElementConnectors { get; set; } = null!;
        public virtual DbSet<TblElementConnectorInstance> TblElementConnectorInstances { get; set; } = null!;
        public virtual DbSet<TblElementInstance> TblElementInstances { get; set; } = null!;
        public virtual DbSet<TblField> TblFields { get; set; } = null!;
        public virtual DbSet<TblGroup> TblGroups { get; set; } = null!;
        public virtual DbSet<TblHistoryJointRelation> TblHistoryJointRelations { get; set; } = null!;
        public virtual DbSet<TblLink> TblLinks { get; set; } = null!;
        public virtual DbSet<TblLinkGroup> TblLinkGroups { get; set; } = null!;
        public virtual DbSet<TblLinkVertex> TblLinkVertexes { get; set; } = null!;
        public virtual DbSet<TblLock> TblLocks { get; set; } = null!;
        public virtual DbSet<TblOptimizationCommand> TblOptimizationCommands { get; set; } = null!;
        public virtual DbSet<TblProject> TblProjects { get; set; } = null!;
        public virtual DbSet<TblProjectRelation> TblProjectRelations { get; set; } = null!;
        public virtual DbSet<TblProjectVersion> TblProjectVersions { get; set; } = null!;
        public virtual DbSet<TblProperty> TblProperties { get; set; } = null!;
        public virtual DbSet<TblPropertyValue> TblPropertyValues { get; set; } = null!;
        public virtual DbSet<TblRecord> TblRecords { get; set; } = null!;
        public virtual DbSet<TblRelativeAssembly> TblRelativeAssemblies { get; set; } = null!;
        public virtual DbSet<TblScheme> TblSchemes { get; set; } = null!;
        public virtual DbSet<TblSection> TblSections { get; set; } = null!;
        public virtual DbSet<TblSpecification> TblSpecifications { get; set; } = null!;
        public virtual DbSet<TblStructure> TblStructures { get; set; } = null!;
        public virtual DbSet<TblTable> TblTables { get; set; } = null!;
        public virtual DbSet<TblTreePath> TblTreePaths { get; set; } = null!;
        public virtual DbSet<TblTrunk> TblTrunks { get; set; } = null!;
        public virtual DbSet<TblTrunkBinding> TblTrunkBindings { get; set; } = null!;
        public virtual DbSet<TblTypeDevice> TblTypeDevices { get; set; } = null!;
        public virtual DbSet<TblUserDeviceParameter> TblUserDeviceParameters { get; set; } = null!;
        public virtual DbSet<TblValue> TblValues { get; set; } = null!;
        public virtual DbSet<TblWire> TblWires { get; set; } = null!;
        public virtual DbSet<TblWireVertex> TblWireVertexes { get; set; } = null!;
        public virtual DbSet<TempChangedDatum> TempChangedData { get; set; } = null!;
        public virtual DbSet<TempDataTable> TempDataTables { get; set; } = null!;
        public virtual DbSet<ViewProjectVersion> ViewProjectVersions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DbMigtation>(entity =>
            {
                entity.HasKey(e => e.Key)
                    .HasName("idx_DbMigtations_primaryKey");

                entity.Property(e => e.Key).HasMaxLength(64);
            });

            modelBuilder.Entity<DbversionTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DBVersionTable");

                entity.Property(e => e.CurrentVersion).HasColumnName("currentVersion");

                entity.Property(e => e.StructureNo).HasDefaultValueSql("((1))");

                entity.Property(e => e.VersionNo).HasDefaultValueSql("((1))");
            });

            modelBuilder.Entity<DeviceParameterSet>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("DeviceParameterSet");

                entity.Property(e => e.DeviceParameterId).HasColumnName("deviceParameterId");

                entity.Property(e => e.DeviceParameterName)
                    .HasColumnType("text")
                    .HasColumnName("deviceParameterName");

                entity.Property(e => e.DeviceParameterTable).HasColumnName("deviceParameterTable");

                entity.Property(e => e.DeviceParameterType)
                    .HasMaxLength(20)
                    .HasColumnName("deviceParameterType");

                entity.Property(e => e.DeviceValueContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("deviceValueContent");

                entity.Property(e => e.DeviceValueDevice).HasColumnName("deviceValueDevice");

                entity.Property(e => e.DeviceValueId).HasColumnName("deviceValueId");

                entity.Property(e => e.DeviceValueReference).HasColumnName("deviceValueReference");

                entity.Property(e => e.ReferenceContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("referenceContent");
            });

            modelBuilder.Entity<DwgByType>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("DwgByType");

                entity.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<GetCategoryDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetCategoryData");

                entity.Property(e => e.DeviceCategoryParameterCategory).HasColumnName("deviceCategoryParameterCategory");

                entity.Property(e => e.DeviceCategoryParameterContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("deviceCategoryParameterContent");

                entity.Property(e => e.DeviceCategoryParameterId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("deviceCategoryParameterId");

                entity.Property(e => e.DeviceCategoryParameterIsRequired).HasColumnName("deviceCategoryParameterIsRequired");

                entity.Property(e => e.DeviceCategoryParameterOrder).HasColumnName("deviceCategoryParameterOrder");

                entity.Property(e => e.DeviceCategoryParameterParameter).HasColumnName("deviceCategoryParameterParameter");

                entity.Property(e => e.DeviceCategoryParameterReference).HasColumnName("deviceCategoryParameterReference");

                entity.Property(e => e.ReferenceContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("referenceContent");
            });

            modelBuilder.Entity<GetJoinCategoryAndDevice>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("GetJoinCategoryAndDevice");

                entity.Property(e => e.CreferenceContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("creferenceContent");

                entity.Property(e => e.DeviceCategoryParameterCategory).HasColumnName("deviceCategoryParameterCategory");

                entity.Property(e => e.DeviceParameterId).HasColumnName("deviceParameterId");

                entity.Property(e => e.DeviceParameterName)
                    .HasColumnType("text")
                    .HasColumnName("deviceParameterName");

                entity.Property(e => e.DeviceParameterTable).HasColumnName("deviceParameterTable");

                entity.Property(e => e.DeviceParameterType)
                    .HasMaxLength(20)
                    .HasColumnName("deviceParameterType");

                entity.Property(e => e.DeviceValueContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("deviceValueContent");

                entity.Property(e => e.DeviceValueDevice).HasColumnName("deviceValueDevice");

                entity.Property(e => e.DeviceValueReference).HasColumnName("deviceValueReference");

                entity.Property(e => e.ReferenceContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("referenceContent");
            });

            modelBuilder.Entity<NotSuitParameter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("NotSuitParameters");

                entity.Property(e => e.DeviceCategoryParameterCategory).HasColumnName("deviceCategoryParameterCategory");

                entity.Property(e => e.DeviceValueDevice).HasColumnName("deviceValueDevice");

                entity.Property(e => e.NotSuitParameter1).HasColumnName("notSuitParameter");
            });

            modelBuilder.Entity<SelectFieldDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("selectFieldData");

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .HasColumnName("tableName");

                entity.Property(e => e.ValueData)
                    .HasColumnType("sql_variant")
                    .HasColumnName("valueData");

                entity.Property(e => e.ValueField).HasColumnName("valueField");

                entity.Property(e => e.ValueId).HasColumnName("valueId");

                entity.Property(e => e.ValueTable).HasColumnName("valueTable");
            });

            modelBuilder.Entity<SuitParameter>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("SuitParameters");

                entity.Property(e => e.DeviceCategoryParameterCategory).HasColumnName("deviceCategoryParameterCategory");

                entity.Property(e => e.DeviceValueDevice).HasColumnName("deviceValueDevice");

                entity.Property(e => e.SuitParameter1).HasColumnName("SuitParameter");
            });

            modelBuilder.Entity<TblAccessoryMatrix>(entity =>
            {
                entity.HasKey(e => e.AccessoryMatrixId)
                    .HasName("idx_tblAccessoryMatrix_primaryKey");

                entity.ToTable("tblAccessoryMatrix");

                entity.HasIndex(e => e.AccessoryMatrixFrom, "IX_tblAccessoryMatrix_tblDeviceAccessories_accessoryMatrixFrom");

                entity.HasIndex(e => e.AccessoryMatrixTo, "IX_tblAccessoryMatrix_tblDeviceAccessories_accessoryMatrixTo");

                entity.HasIndex(e => e.AccessoryMatrixDevice, "IX_tblAccessoryMatrix_tblDevices_accessoryMatrixDevice");

                entity.HasIndex(e => new { e.AccessoryMatrixDevice, e.AccessoryMatrixFrom, e.AccessoryMatrixTo }, "idx_tblAccessoryMatrix_accessoryMatrixDevice_accessoryMatrixFrom_accessoryMatrixTo");

                entity.Property(e => e.AccessoryMatrixId).HasColumnName("accessoryMatrixId");

                entity.Property(e => e.AccessoryMatrixActual).HasColumnName("accessoryMatrixActual");

                entity.Property(e => e.AccessoryMatrixDevice).HasColumnName("accessoryMatrixDevice");

                entity.Property(e => e.AccessoryMatrixFrom).HasColumnName("accessoryMatrixFrom");

                entity.Property(e => e.AccessoryMatrixTo).HasColumnName("accessoryMatrixTo");

                entity.Property(e => e.AccessoryMatrixType).HasColumnName("accessoryMatrixType");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.AccessoryMatrixDeviceNavigation)
                    .WithMany(p => p.TblAccessoryMatrices)
                    .HasForeignKey(d => d.AccessoryMatrixDevice)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.AccessoryMatrixFromNavigation)
                    .WithMany(p => p.TblAccessoryMatricesAccessoryMatricesFromNavigation)
                    .HasForeignKey(d => d.AccessoryMatrixFrom)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.AccessoryMatrixToNavigation)
                    .WithMany(p => p.TblAccessoryMatricesAccessoryMatricesToNavigation)
                    .HasForeignKey(d => d.AccessoryMatrixTo);
            });

            modelBuilder.Entity<TblArray>(entity =>
            {
                entity.ToTable("TblArray");

                entity.HasIndex(e => e.TblArrayId, "IX_TblArray_TblArrayId");

                entity.HasIndex(e => e.TblRelativeAssemblyId, "IX_TblArray_TblRelativeAssemblyId");

                entity.Property(e => e.Xcount).HasColumnName("XCount");

                entity.Property(e => e.Xdirection).HasColumnName("XDirection");

                entity.Property(e => e.Ycount).HasColumnName("YCount");

                entity.Property(e => e.Ydirection).HasColumnName("YDirection");

                entity.Property(e => e.Zcount).HasColumnName("ZCount");

                entity.Property(e => e.Zdirection).HasColumnName("ZDirection");

                entity.HasOne(d => d.TblArrayNavigation)
                    .WithMany(p => p.InverseTblArrayNavigation)
                    .HasForeignKey(d => d.TblArrayId);

                entity.HasOne(d => d.TblRelativeAssembly)
                    .WithMany(p => p.TblArrays)
                    .HasForeignKey(d => d.TblRelativeAssemblyId);
            });

            modelBuilder.Entity<TblBusVertex>(entity =>
            {
                entity.HasKey(e => e.VertexId)
                    .HasName("idx_tblBusVertexes_primaryKey");

                entity.ToTable("tblBusVertexes");

                entity.HasIndex(e => e.VertexBus, "idx_tblBusVertexes_vertexBus");

                entity.Property(e => e.VertexId).HasColumnName("vertexId");

                entity.Property(e => e.VertexBus).HasColumnName("vertexBus");

                entity.Property(e => e.VertexX).HasColumnName("vertexX");

                entity.Property(e => e.VertexY).HasColumnName("vertexY");

                entity.Property(e => e.VertexZ).HasColumnName("vertexZ");

                entity.HasOne(d => d.VertexBusNavigation)
                    .WithMany(p => p.TblBusVertices)
                    .HasForeignKey(d => d.VertexBus)
                    .HasConstraintName("idx_tblBusVertexes_tblBuses");
            });

            modelBuilder.Entity<TblBuse>(entity =>
            {
                entity.HasKey(e => e.BusId)
                    .HasName("idx_tblBuses_primaryKey");

                entity.ToTable("tblBuses");

                entity.HasIndex(e => new { e.BusId, e.BusProjectVersion }, "idx_tblBuses_busId_busProjectVersion");

                entity.HasIndex(e => new { e.BusProjectVersion, e.BusActual }, "idx_tblBuses_busProjectVersion_busActual");

                entity.Property(e => e.BusId).HasColumnName("busId");

                entity.Property(e => e.BusActual).HasColumnName("busActual");

                entity.Property(e => e.BusCurrent).HasColumnName("busCurrent");

                entity.Property(e => e.BusHeight).HasColumnName("busHeight");

                entity.Property(e => e.BusInstance).HasColumnName("busInstance");

                entity.Property(e => e.BusPerPhase).HasColumnName("busPerPhase");

                entity.Property(e => e.BusPoluses).HasColumnName("busPoluses");

                entity.Property(e => e.BusProjectVersion).HasColumnName("busProjectVersion");

                entity.Property(e => e.BusWidth).HasColumnName("busWidth");

                entity.HasOne(d => d.BusProjectVersionNavigation)
                    .WithMany(p => p.TblBuses)
                    .HasForeignKey(d => d.BusProjectVersion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblBuses_tblProjectVersions");
            });

            modelBuilder.Entity<TblCabinet>(entity =>
            {
                entity.HasKey(e => e.CabinetId)
                    .HasName("idx_tblCabinets_primaryKey");

                entity.ToTable("tblCabinets");

                entity.HasIndex(e => new { e.CabinetId, e.CabinetProjectVersion }, "idx_tblCabinets_cabinetId_cabinetProjectVersion");

                entity.HasIndex(e => new { e.CabinetProjectVersion, e.CabinetActual }, "idx_tblCabinets_cabinetProjectVersion_cabinetActual");

                entity.Property(e => e.CabinetId).HasColumnName("cabinetId");

                entity.Property(e => e.CabinetActual).HasColumnName("cabinetActual");

                entity.Property(e => e.CabinetInstance).HasColumnName("cabinetInstance");

                entity.Property(e => e.CabinetPermanently)
                    .HasColumnName("cabinetPermanently")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CabinetPlacement)
                    .HasColumnName("cabinetPlacement")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CabinetProjectVersion).HasColumnName("cabinetProjectVersion");

                entity.Property(e => e.CabinetSignature)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("cabinetSignature");

                entity.Property(e => e.CabinetTemperatureMax)
                    .HasColumnName("cabinetTemperatureMax")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CabinetTemperatureMin)
                    .HasColumnName("cabinetTemperatureMin")
                    .HasDefaultValueSql("((0))");

                entity.HasOne(d => d.CabinetProjectVersionNavigation)
                    .WithMany(p => p.TblCabinets)
                    .HasForeignKey(d => d.CabinetProjectVersion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblCabinets_tblProjectVersions");
            });

            modelBuilder.Entity<TblCategory>(entity =>
            {
                entity.HasKey(e => e.CategoryId)
                    .HasName("idx_tblCategories_primaryKey");

                entity.ToTable("tblCategories");

                entity.Property(e => e.CategoryId).HasColumnName("categoryId");

                entity.Property(e => e.CategoryCustomer)
                    .HasColumnName("categoryCustomer")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .HasColumnName("categoryName");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<TblChangedItem>(entity =>
            {
                entity.ToTable("tblChangedItems");

                entity.HasIndex(e => e.VersionNo, "IX_CI_VersionNo");

                entity.HasIndex(e => e.OpType, "IX_CI_opType");

                entity.HasIndex(e => e.TblId, "IX_CI_tblId");

                entity.HasIndex(e => e.TblName, "IX_CI_tblName");

                entity.Property(e => e.Dt)
                    .HasColumnType("datetime")
                    .HasColumnName("dt")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.OpType).HasColumnName("opType");

                entity.Property(e => e.TblId).HasColumnName("tblId");

                entity.Property(e => e.TblName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("tblName");
            });

            modelBuilder.Entity<TblChangedSqlHistory>(entity =>
            {
                entity.ToTable("tblChangedSqlHistory");

                entity.Property(e => e.SqlText).HasColumnName("sqlText");
            });

            modelBuilder.Entity<TblCluster>(entity =>
            {
                entity.HasKey(e => e.ClusterId)
                    .HasName("idx_tblClusters_primaryKey");

                entity.ToTable("tblClusters");

                entity.HasIndex(e => e.ClusterStructure, "IX_tblClusters_tblStructure_clusterStructure");

                entity.Property(e => e.ClusterId).HasColumnName("clusterId");

                entity.Property(e => e.ClusterName)
                    .HasMaxLength(255)
                    .HasColumnName("clusterName");

                entity.Property(e => e.ClusterStructure).HasColumnName("clusterStructure");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ClusterStructureNavigation)
                    .WithMany(p => p.TblClusters)
                    .HasForeignKey(d => d.ClusterStructure)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TblDbDevice>(entity =>
            {
                entity.HasKey(e => e.EntityKey);

                entity.Property(e => e.EntityKey).ValueGeneratedNever();
            });

            modelBuilder.Entity<TblDbversionPrice>(entity =>
            {
                entity.ToTable("TblDBVersionPrices");
            });

            modelBuilder.Entity<TblDecoration>(entity =>
            {
                entity.HasKey(e => e.DecorationId)
                    .HasName("idx_tblDecorations_primaryKey");

                entity.ToTable("tblDecorations");

                entity.HasIndex(e => new { e.DecorationProjectVersion, e.DecorationActual }, "idx_tblDecorations_decorationProjectVersion_decorationActual");

                entity.Property(e => e.DecorationId).HasColumnName("decorationId");

                entity.Property(e => e.DecorationActual).HasColumnName("decorationActual");

                entity.Property(e => e.DecorationInstance).HasColumnName("decorationInstance");

                entity.Property(e => e.DecorationProjectVersion).HasColumnName("decorationProjectVersion");

                entity.Property(e => e.DecorationScheme).HasColumnName("decorationScheme");

                entity.HasOne(d => d.DecorationProjectVersionNavigation)
                    .WithMany(p => p.TblDecorations)
                    .HasForeignKey(d => d.DecorationProjectVersion)
                    .HasConstraintName("idx_tblDecorations_tblProjectVersions");
            });

            modelBuilder.Entity<TblDevice>(entity =>
            {
                entity.HasKey(e => e.DeviceId)
                    .HasName("idx_tblDevices_primaryKey");

                entity.ToTable("tblDevices");

                entity.HasIndex(e => e.TblRelativeAssemblyId, "IX_TblDevices_TblRelativeAssemblyId");

                entity.HasIndex(e => e.TypeDeviceId, "IX_TblDevices_TypeDeviceId");

                entity.Property(e => e.DeviceId).HasColumnName("deviceId");

                entity.Property(e => e.Code).HasDefaultValueSql("(N'')");

                entity.Property(e => e.DeviceType).HasColumnName("deviceType");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.TblRelativeAssembly)
                    .WithMany(p => p.TblDevices)
                    .HasForeignKey(d => d.TblRelativeAssemblyId)
                    .HasConstraintName("FK_TblDevices_TblRelativeAssemblies_TblRelativeAssemblyId");

                entity.HasOne(d => d.TypeDevice)
                    .WithMany(p => p.TblDevices)
                    .HasForeignKey(d => d.TypeDeviceId)
                    .HasConstraintName("FK_TblDevices_TblTypeDevice_TypeDeviceId");
            });

            modelBuilder.Entity<TblDeviceAccessory>(entity =>
            {
                entity.HasKey(e => e.DeviceAccessoryId)
                    .HasName("idx_tblDeviceAccessories_primaryKey");

                entity.ToTable("tblDeviceAccessories");

                entity.HasIndex(e => e.DeviceAccessoryMaster, "IX_tblDeviceAccessories_tblDevices_deviceAccessoryMaster");

                entity.HasIndex(e => e.DeviceAccessorySlave, "IX_tblDeviceAccessories_tblDevices_deviceAccessorySlave");

                entity.HasIndex(e => new { e.DeviceAccessoryMaster, e.DeviceAccessorySlave }, "idx_tblDeviceAccessories_deviceAccessoryMaster_deviceAccessorySlave");

                entity.Property(e => e.DeviceAccessoryId).HasColumnName("deviceAccessoryId");

                entity.Property(e => e.DeviceAccessoryActual).HasColumnName("deviceAccessoryActual");

                entity.Property(e => e.DeviceAccessoryAngleXy).HasColumnName("deviceAccessoryAngleXY");

                entity.Property(e => e.DeviceAccessoryAngleXz).HasColumnName("deviceAccessoryAngleXZ");

                entity.Property(e => e.DeviceAccessoryAngleYz).HasColumnName("deviceAccessoryAngleYZ");

                entity.Property(e => e.DeviceAccessoryInvisible)
                    .HasColumnName("deviceAccessoryInvisible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.DeviceAccessoryJointMaster).HasColumnName("deviceAccessoryJointMaster");

                entity.Property(e => e.DeviceAccessoryJointSlave).HasColumnName("deviceAccessoryJointSlave");

                entity.Property(e => e.DeviceAccessoryMaster).HasColumnName("deviceAccessoryMaster");

                entity.Property(e => e.DeviceAccessoryQuantity).HasColumnName("deviceAccessoryQuantity");

                entity.Property(e => e.DeviceAccessorySlave).HasColumnName("deviceAccessorySlave");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.DeviceAccessoryMasterNavigation)
                    .WithMany(p => p.TblDeviceAccessoryDeviceAccessoryMasterNavigations)
                    .HasForeignKey(d => d.DeviceAccessoryMaster)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.DeviceAccessorySlaveNavigation)
                    .WithMany(p => p.TblDeviceAccessoryDeviceAccessorySlaveNavigations)
                    .HasForeignKey(d => d.DeviceAccessorySlave);
            });

            modelBuilder.Entity<TblDeviceCategoryParameter>(entity =>
            {
                entity.HasKey(e => e.DeviceCategoryParameterId)
                    .HasName("PK__tblDevic__C366F194D9BDF052");

                entity.ToTable("tblDeviceCategoryParameters");

                entity.HasIndex(e => e.DeviceCategoryParameterReference, "IX_tblDeviceCategoryParameters_tblValues_deviceCategoryParameterReference");

                entity.HasIndex(e => new { e.DeviceCategoryParameterParameter, e.DeviceCategoryParameterCategory }, "idx_tblDeviceCategoryParameter_uq")
                    .IsUnique();

                entity.Property(e => e.DeviceCategoryParameterId).HasColumnName("deviceCategoryParameterId");

                entity.Property(e => e.DeviceCategoryParameterCategory).HasColumnName("deviceCategoryParameterCategory");

                entity.Property(e => e.DeviceCategoryParameterContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("deviceCategoryParameterContent");

                entity.Property(e => e.DeviceCategoryParameterIsRequired).HasColumnName("deviceCategoryParameterIsRequired");

                entity.Property(e => e.DeviceCategoryParameterOrder).HasColumnName("deviceCategoryParameterOrder");

                entity.Property(e => e.DeviceCategoryParameterParameter).HasColumnName("deviceCategoryParameterParameter");

                entity.Property(e => e.DeviceCategoryParameterReference).HasColumnName("deviceCategoryParameterReference");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.DeviceCategoryParameterCategoryNavigation)
                    .WithMany(p => p.TblDeviceCategoryParameters)
                    .HasForeignKey(d => d.DeviceCategoryParameterCategory)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDeviceCategoryParam_tblCategories_fk");

                entity.HasOne(d => d.DeviceCategoryParameterReferenceNavigation)
                    .WithMany(p => p.TblDeviceCategoryParameters)
                    .HasForeignKey(d => d.DeviceCategoryParameterReference)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TblDeviceParameter>(entity =>
            {
                entity.HasKey(e => e.DeviceParameterId)
                    .HasName("idx_tblDeviceParameters_primaryKey");

                entity.ToTable("tblDeviceParameters");

                entity.HasIndex(e => e.DeviceParameterTable, "IX_tblDeviceParameters_tblTables_deviceParameterTable");

                entity.Property(e => e.DeviceParameterId).HasColumnName("deviceParameterId");

                entity.Property(e => e.DeviceParameterIsAccessory).HasColumnName("deviceParameterIsAccessory");

                entity.Property(e => e.DeviceParameterIsAssembly).HasColumnName("deviceParameterIsAssembly");

                entity.Property(e => e.DeviceParameterIsBus).HasColumnName("deviceParameterIsBus");

                entity.Property(e => e.DeviceParameterIsBusAssembly).HasColumnName("deviceParameterIsBusAssembly");

                entity.Property(e => e.DeviceParameterIsCabinet).HasColumnName("deviceParameterIsCabinet");

                entity.Property(e => e.DeviceParameterIsEquipment).HasColumnName("deviceParameterIsEquipment");

                entity.Property(e => e.DeviceParameterName)
                    .HasColumnType("text")
                    .HasColumnName("deviceParameterName");

                entity.Property(e => e.DeviceParameterTable).HasColumnName("deviceParameterTable");

                entity.Property(e => e.DeviceParameterType)
                    .HasMaxLength(20)
                    .HasColumnName("deviceParameterType");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.DeviceParameterTableNavigation)
                    .WithMany(p => p.TblDeviceParameters)
                    .HasForeignKey(d => d.DeviceParameterTable)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TblDeviceValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblDeviceValues");

                entity.HasIndex(e => e.DeviceValueParameter, "IX_tblDeviceValues_tblDeviceParameters_deviceValueParameter");

                entity.HasIndex(e => new { e.DeviceValueDevice, e.DeviceValueParameter, e.DeviceValueReference, e.DeviceValueContent }, "idx_tblDeviceValues_deviceValueDevice_deviceValueParameter_deviceValueReference_deviceValueContent");

                entity.Property(e => e.DeviceValueContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("deviceValueContent");

                entity.Property(e => e.DeviceValueDevice).HasColumnName("deviceValueDevice");

                entity.Property(e => e.DeviceValueId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("deviceValueId");

                entity.Property(e => e.DeviceValueParameter).HasColumnName("deviceValueParameter");

                entity.Property(e => e.DeviceValueReference).HasColumnName("deviceValueReference");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.DeviceValueDeviceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceValueDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDevices_tblDeviceValues_fk");

                entity.HasOne(d => d.DeviceValueParameterNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceValueParameter)
                    .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(d => d.DeviceValueReferenceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.DeviceValueReference)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TblDirectorySystem>(entity =>
            {
                entity.ToTable("TblDirectorySystem");
            });

            modelBuilder.Entity<TblDrawingAssembly>(entity =>
            {
                entity.HasKey(e => e.AssemblyId)
                    .HasName("idx_tblDrawingAssemblies_primaryKey");

                entity.ToTable("tblDrawingAssemblies");

                entity.Property(e => e.AssemblyId).HasColumnName("assemblyId");

                entity.Property(e => e.AssemblyName)
                    .HasColumnType("text")
                    .HasColumnName("assemblyName");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");
            });

            modelBuilder.Entity<TblDrawingAttachment>(entity =>
            {
                entity.HasKey(e => e.AttachmentId)
                    .HasName("idx_tblDrawingAttachments_primaryKey");

                entity.ToTable("tblDrawingAttachments");

                entity.HasIndex(e => e.AttachmentDevice, "idx_tblDrawingAttachments_attachmentDevice");

                entity.Property(e => e.AttachmentId).HasColumnName("attachmentId");

                entity.Property(e => e.AttachmentDevice).HasColumnName("attachmentDevice");

                entity.Property(e => e.AttachmentDocument)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("attachmentDocument");

                entity.HasOne(d => d.AttachmentDeviceNavigation)
                    .WithMany(p => p.TblDrawingAttachments)
                    .HasForeignKey(d => d.AttachmentDevice)
                    .HasConstraintName("idx_tblDrawingAttachments_tblDevices");
            });

            modelBuilder.Entity<TblDrawingConductor>(entity =>
            {
                entity.HasKey(e => e.ConductorId)
                    .HasName("idx_tblDrawingConductors_primaryKey");

                entity.ToTable("tblDrawingConductors");

                entity.HasIndex(e => e.ConductorDevice, "idx_tblDrawingConductors_conductorDevice");

                entity.Property(e => e.ConductorId).HasColumnName("conductorId");

                entity.Property(e => e.ConductorAngle).HasColumnName("conductorAngle");

                entity.Property(e => e.ConductorDevice).HasColumnName("conductorDevice");

                entity.Property(e => e.ConductorHeight).HasColumnName("conductorHeight");

                entity.Property(e => e.ConductorPhases).HasColumnName("conductorPhases");

                entity.Property(e => e.ConductorWidth).HasColumnName("conductorWidth");

                entity.Property(e => e.ConductorWires).HasColumnName("conductorWires");
            });

            modelBuilder.Entity<TblDrawingConductorWire>(entity =>
            {
                entity.HasKey(e => e.ConductorWireId)
                    .HasName("idx_tblDrawingConductorWires_primaryKey");

                entity.ToTable("tblDrawingConductorWires");

                entity.Property(e => e.ConductorWireId).HasColumnName("conductorWireId");

                entity.Property(e => e.ConductorWireActual).HasColumnName("conductorWireActual");

                entity.Property(e => e.ConductorWireConductor).HasColumnName("conductorWireConductor");

                entity.Property(e => e.ConductorWirePhase).HasColumnName("conductorWirePhase");

                entity.Property(e => e.ConductorWirePositionX).HasColumnName("conductorWirePositionX");

                entity.Property(e => e.ConductorWirePositionY).HasColumnName("conductorWirePositionY");

                entity.HasOne(d => d.ConductorWireConductorNavigation)
                    .WithMany(p => p.TblDrawingConductorWires)
                    .HasForeignKey(d => d.ConductorWireConductor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingConductorWires_tblDrawingConductors");
            });

            modelBuilder.Entity<TblDrawingConnector>(entity =>
            {
                entity.HasKey(e => e.ConnectorId)
                    .HasName("idx_tblDrawingConnectors_primaryKey");

                entity.ToTable("tblDrawingConnectors");

                entity.HasIndex(e => e.ConnectorDevice, "idx_tblDrawingConnectors_connectorDevice");

                entity.Property(e => e.ConnectorId).HasColumnName("connectorId");

                entity.Property(e => e.ConnectorActual).HasColumnName("connectorActual");

                entity.Property(e => e.ConnectorContent).HasColumnName("connectorContent");

                entity.Property(e => e.ConnectorDevice).HasColumnName("connectorDevice");

                entity.Property(e => e.ConnectorDirection).HasColumnName("connectorDirection");

                entity.Property(e => e.ConnectorLabel)
                    .HasColumnType("text")
                    .HasColumnName("connectorLabel");

                entity.Property(e => e.ConnectorLabelDirection).HasColumnName("connectorLabelDirection");

                entity.Property(e => e.ConnectorLabelOrientation).HasColumnName("connectorLabelOrientation");

                entity.Property(e => e.ConnectorLabelX).HasColumnName("connectorLabelX");

                entity.Property(e => e.ConnectorLabelY).HasColumnName("connectorLabelY");

                entity.Property(e => e.ConnectorPositionX).HasColumnName("connectorPositionX");

                entity.Property(e => e.ConnectorPositionY).HasColumnName("connectorPositionY");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ConnectorDeviceNavigation)
                    .WithMany(p => p.TblDrawingConnectors)
                    .HasForeignKey(d => d.ConnectorDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingConnectors_tblDevices");
            });

            modelBuilder.Entity<TblDrawingContainer>(entity =>
            {
                entity.HasKey(e => e.ContainerId)
                    .HasName("idx_tblDrawingContainers_primaryKey");

                entity.ToTable("tblDrawingContainers");

                entity.HasIndex(e => e.ContainerSolution, "idx_tblDrawingContainers_containerSolution");

                entity.Property(e => e.ContainerId).HasColumnName("containerId");

                entity.Property(e => e.ContainerActual).HasColumnName("containerActual");

                entity.Property(e => e.ContainerAngleXy).HasColumnName("containerAngleXY");

                entity.Property(e => e.ContainerAngleXz).HasColumnName("containerAngleXZ");

                entity.Property(e => e.ContainerAngleYz).HasColumnName("containerAngleYZ");

                entity.Property(e => e.ContainerDevice).HasColumnName("containerDevice");

                entity.Property(e => e.ContainerHandleX).HasColumnName("containerHandleX");

                entity.Property(e => e.ContainerHandleY).HasColumnName("containerHandleY");

                entity.Property(e => e.ContainerHandleZ).HasColumnName("containerHandleZ");

                entity.Property(e => e.ContainerPositionX).HasColumnName("containerPositionX");

                entity.Property(e => e.ContainerPositionY).HasColumnName("containerPositionY");

                entity.Property(e => e.ContainerPositionZ).HasColumnName("containerPositionZ");

                entity.Property(e => e.ContainerRemark)
                    .IsUnicode(false)
                    .HasColumnName("containerRemark");

                entity.Property(e => e.ContainerSolution).HasColumnName("containerSolution");

                entity.HasOne(d => d.ContainerSolutionNavigation)
                    .WithMany(p => p.TblDrawingContainers)
                    .HasForeignKey(d => d.ContainerSolution)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingContainers_tblDrawingSolutions");
            });

            modelBuilder.Entity<TblDrawingContent>(entity =>
            {
                entity.HasKey(e => e.ContentId)
                    .HasName("idx_tblDrawingContents_primaryKey");

                entity.ToTable("tblDrawingContents");

                entity.HasIndex(e => e.ContentDevice, "idx_tblDrawingContents_contentDevice");

                entity.Property(e => e.ContentId).HasColumnName("contentId");

                entity.Property(e => e.ContentActual).HasColumnName("contentActual");

                entity.Property(e => e.ContentAngleHorizontal).HasColumnName("contentAngleHorizontal");

                entity.Property(e => e.ContentAngleVertical).HasColumnName("contentAngleVertical");

                entity.Property(e => e.ContentDevice).HasColumnName("contentDevice");

                entity.Property(e => e.ContentDocument)
                    .HasColumnType("text")
                    .HasColumnName("contentDocument");

                entity.Property(e => e.ContentHandleX).HasColumnName("contentHandleX");

                entity.Property(e => e.ContentHandleY).HasColumnName("contentHandleY");

                entity.Property(e => e.ContentHandleZ).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.Property(e => e.ContentLocationMaxX).HasColumnName("contentLocationMaxX");

                entity.Property(e => e.ContentLocationMaxY).HasColumnName("contentLocationMaxY");

                entity.Property(e => e.ContentLocationMaxZ).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.Property(e => e.ContentLocationMinX).HasColumnName("contentLocationMinX");

                entity.Property(e => e.ContentLocationMinY).HasColumnName("contentLocationMinY");

                entity.Property(e => e.ContentLocationMinZ).HasDefaultValueSql("((0.000000000000000e+000))");

                entity.Property(e => e.ContentMirrored).HasColumnName("contentMirrored");

                entity.Property(e => e.ContentScheme).HasColumnName("contentScheme");

                entity.Property(e => e.ContentType).HasColumnName("contentType");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.HasOne(d => d.ContentDeviceNavigation)
                    .WithMany(p => p.TblDrawingContents)
                    .HasForeignKey(d => d.ContentDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingContents_tblDevices");
            });

            modelBuilder.Entity<TblDrawingJoint>(entity =>
            {
                entity.HasKey(e => e.JointId)
                    .HasName("idx_tblDrawingJoints_primaryKey");

                entity.ToTable("tblDrawingJoints");

                entity.HasIndex(e => e.JointDevice, "idx_tblDrawingJoints_jointDevice");

                entity.Property(e => e.JointId).HasColumnName("jointId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.JointActual).HasColumnName("jointActual");

                entity.Property(e => e.JointCode).HasColumnName("jointCode");

                entity.Property(e => e.JointDevice).HasColumnName("jointDevice");

                entity.Property(e => e.JointLabel)
                    .HasColumnType("text")
                    .HasColumnName("jointLabel");

                entity.Property(e => e.JointPositionX).HasColumnName("jointPositionX");

                entity.Property(e => e.JointPositionY).HasColumnName("jointPositionY");

                entity.Property(e => e.JointPositionZ).HasColumnName("jointPositionZ");

                entity.Property(e => e.JointType).HasColumnName("jointType");

                entity.HasOne(d => d.JointDeviceNavigation)
                    .WithMany(p => p.TblDrawingJoints)
                    .HasForeignKey(d => d.JointDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingJoints_tblDevices");
            });

            modelBuilder.Entity<TblDrawingJointRelation>(entity =>
            {
                entity.HasKey(e => e.JointRelationId)
                    .HasName("PK__tblDrawi__C0659742F2ED794D");

                entity.ToTable("tblDrawingJointRelations");

                entity.HasIndex(e => new { e.JointRelationFirstJoint, e.JointRelationSecondJoint, e.JointRelationPlacement }, "idx_tblDrawingJointRelations_jointDeviceJointPlacement")
                    .IsUnique();

                entity.HasIndex(e => e.JointRelationFirstJoint, "idx_tblDrawingJointRelations_jointRelationFirstJoint");

                entity.HasIndex(e => e.JointRelationSecondJoint, "idx_tblDrawingJointRelations_jointRelationSecondJoint");

                entity.Property(e => e.JointRelationId).HasColumnName("jointRelationId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.JointRelationFirstJoint).HasColumnName("jointRelationFirstJoint");

                entity.Property(e => e.JointRelationPlacement).HasColumnName("jointRelationPlacement");

                entity.Property(e => e.JointRelationSecondJoint).HasColumnName("jointRelationSecondJoint");

                entity.HasOne(d => d.JointRelationFirstJointNavigation)
                    .WithMany(p => p.TblDrawingJointRelationJointRelationFirstJointNavigations)
                    .HasForeignKey(d => d.JointRelationFirstJoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idx_tblDrawingJointRelations_tblDrawingJoints_jointRelationFirstJoint");

                entity.HasOne(d => d.JointRelationSecondJointNavigation)
                    .WithMany(p => p.TblDrawingJointRelationJointRelationSecondJointNavigations)
                    .HasForeignKey(d => d.JointRelationSecondJoint)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("idx_tblDrawingJointRelations_tblDrawingJoints_jointRelationSecondJoint");
            });

            modelBuilder.Entity<TblDrawingRelation>(entity =>
            {
                entity.HasKey(e => e.RelationId)
                    .HasName("PK__tblDrawi__F0BD8F2601B274DB")
                    .IsClustered(false);

                entity.ToTable("tblDrawingRelations");

                entity.HasIndex(e => e.RelationAssembly, "IX_tblDrawingRelations_tblDevices_relationAssembly");

                entity.HasIndex(e => e.RelationDevice, "IX_tblDrawingRelations_tblDevices_relationDevice");

                entity.HasIndex(e => e.RelationId, "idx_tblDrawingRelations_primaryKey")
                    .IsUnique()
                    .IsClustered()
                    .HasFillFactor(100);

                entity.HasIndex(e => e.RelationAssembly, "idx_tblDrawingRelations_relationAssembly")
                    .HasFillFactor(100);

                entity.HasIndex(e => new { e.RelationAssembly, e.RelationDeviceCode }, "idx_tblDrawingRelations_relationAssembly_relationDeviceCode")
                    .HasFillFactor(100);

                entity.Property(e => e.RelationId).HasColumnName("relationId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.RelationActual).HasColumnName("relationActual");

                entity.Property(e => e.RelationAngleXy).HasColumnName("relationAngleXY");

                entity.Property(e => e.RelationAngleXz).HasColumnName("relationAngleXZ");

                entity.Property(e => e.RelationAngleYz).HasColumnName("relationAngleYZ");

                entity.Property(e => e.RelationAssembly).HasColumnName("relationAssembly");

                entity.Property(e => e.RelationDevice).HasColumnName("relationDevice");

                entity.Property(e => e.RelationDeviceCode).HasColumnName("relationDeviceCode");

                entity.Property(e => e.RelationDeviceParentCode).HasColumnName("relationDeviceParentCode");

                entity.Property(e => e.RelationIsAccessory).HasColumnName("relationIsAccessory");

                entity.Property(e => e.RelationJointCode).HasColumnName("relationJointCode");

                entity.Property(e => e.RelationJointParentCode).HasColumnName("relationJointParentCode");

                entity.Property(e => e.RelationPositionX).HasColumnName("relationPositionX");

                entity.Property(e => e.RelationPositionY).HasColumnName("relationPositionY");

                entity.Property(e => e.RelationPositionZ).HasColumnName("relationPositionZ");

                entity.Property(e => e.RelationQuantity).HasColumnName("relationQuantity");

                entity.HasOne(d => d.RelationAssemblyNavigation)
                    .WithMany(p => p.TblDrawingRelationRelationAssemblyNavigations)
                    .HasForeignKey(d => d.RelationAssembly);

                entity.HasOne(d => d.RelationDeviceNavigation)
                    .WithMany(p => p.TblDrawingRelationRelationDeviceNavigations)
                    .HasForeignKey(d => d.RelationDevice)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            modelBuilder.Entity<TblDrawingSolution>(entity =>
            {
                entity.HasKey(e => e.SolutionId)
                    .HasName("idx_tblDrawingSolutions_primaryKey");

                entity.ToTable("tblDrawingSolutions");

                entity.Property(e => e.SolutionId).HasColumnName("solutionId");

                entity.Property(e => e.SolutionDevice).HasColumnName("solutionDevice");

                entity.HasOne(d => d.SolutionDeviceNavigation)
                    .WithMany(p => p.TblDrawingSolutions)
                    .HasForeignKey(d => d.SolutionDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingSolutions_tblDevices");
            });

            modelBuilder.Entity<TblDrawingTrace>(entity =>
            {
                entity.HasKey(e => e.TraceId)
                    .HasName("idx_tblDrawingTraces_primaryKey");

                entity.ToTable("tblDrawingTraces");

                entity.Property(e => e.TraceId).HasColumnName("traceId");

                entity.Property(e => e.TraceActual).HasColumnName("traceActual");

                entity.Property(e => e.TraceSolution).HasColumnName("traceSolution");

                entity.HasOne(d => d.TraceSolutionNavigation)
                    .WithMany(p => p.TblDrawingTraces)
                    .HasForeignKey(d => d.TraceSolution)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingTraces_tblDrawingSolutions");
            });

            modelBuilder.Entity<TblDrawingTraceVertex>(entity =>
            {
                entity.HasKey(e => e.TraceVertexId)
                    .HasName("idx_tblDrawingTraceVertexes_primaryKey");

                entity.ToTable("tblDrawingTraceVertexes");

                entity.HasIndex(e => e.TraceVertexTrace, "idx_tblDrawingTraceVertexes_traceVertexTrace");

                entity.Property(e => e.TraceVertexId).HasColumnName("traceVertexId");

                entity.Property(e => e.TraceVertexActual).HasColumnName("traceVertexActual");

                entity.Property(e => e.TraceVertexTrace).HasColumnName("traceVertexTrace");

                entity.Property(e => e.TraceVertexX).HasColumnName("traceVertexX");

                entity.Property(e => e.TraceVertexY).HasColumnName("traceVertexY");

                entity.Property(e => e.TraceVertexZ).HasColumnName("traceVertexZ");

                entity.HasOne(d => d.TraceVertexTraceNavigation)
                    .WithMany(p => p.TblDrawingTraceVertices)
                    .HasForeignKey(d => d.TraceVertexTrace)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingTraceVertexes_tblDrawingTraces");
            });

            modelBuilder.Entity<TblDrawingUnit>(entity =>
            {
                entity.HasKey(e => e.UnitId)
                    .HasName("idx_tblDrawingUnits_primaryKey");

                entity.ToTable("tblDrawingUnits");

                entity.HasIndex(e => e.UnitDevice, "idx_tblDrawingUnits_unitDevice");

                entity.Property(e => e.UnitId).HasColumnName("unitId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.UnitBoundMaxX).HasColumnName("unitBoundMaxX");

                entity.Property(e => e.UnitBoundMaxY).HasColumnName("unitBoundMaxY");

                entity.Property(e => e.UnitBoundMaxZ).HasColumnName("unitBoundMaxZ");

                entity.Property(e => e.UnitBoundMinX).HasColumnName("unitBoundMinX");

                entity.Property(e => e.UnitBoundMinY).HasColumnName("unitBoundMinY");

                entity.Property(e => e.UnitBoundMinZ).HasColumnName("unitBoundMinZ");

                entity.Property(e => e.UnitDevice).HasColumnName("unitDevice");

                entity.Property(e => e.UnitMountingMaxX).HasColumnName("unitMountingMaxX");

                entity.Property(e => e.UnitMountingMaxY).HasColumnName("unitMountingMaxY");

                entity.Property(e => e.UnitMountingMaxZ).HasColumnName("unitMountingMaxZ");

                entity.Property(e => e.UnitMountingMinX).HasColumnName("unitMountingMinX");

                entity.Property(e => e.UnitMountingMinY).HasColumnName("unitMountingMinY");

                entity.Property(e => e.UnitMountingMinZ).HasColumnName("unitMountingMinZ");

                entity.HasOne(d => d.UnitDeviceNavigation)
                    .WithMany(p => p.TblDrawingUnits)
                    .HasForeignKey(d => d.UnitDevice)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblDrawingUnits_tblDevices");
            });

            modelBuilder.Entity<TblElement>(entity =>
            {
                entity.HasKey(e => e.ElementId)
                    .HasName("idx_tblElements_primaryKey");

                entity.ToTable("tblElements");

                entity.HasIndex(e => new { e.ElementId, e.ElementProjectVersion }, "idx_tblElements_elementId_elementProjectVersion");

                entity.HasIndex(e => new { e.ElementProjectVersion, e.ElementActual }, "idx_tblElements_elementProjectVersion_elementActual");

                entity.Property(e => e.ElementId).HasColumnName("elementId");

                entity.Property(e => e.ElementActual).HasColumnName("elementActual");

                entity.Property(e => e.ElementAttachmentFileName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("elementAttachmentFileName")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ElementBoundMaxX)
                    .HasColumnName("elementBoundMaxX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementBoundMaxY)
                    .HasColumnName("elementBoundMaxY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementBoundMaxZ)
                    .HasColumnName("elementBoundMaxZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementBoundMinX)
                    .HasColumnName("elementBoundMinX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementBoundMinY)
                    .HasColumnName("elementBoundMinY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementBoundMinZ)
                    .HasColumnName("elementBoundMinZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementContentAngleXy)
                    .HasColumnName("elementContentAngleXY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementContentAngleXz)
                    .HasColumnName("elementContentAngleXZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementContentAngleYz)
                    .HasColumnName("elementContentAngleYZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementContentFileName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("elementContentFileName")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ElementContentMirrored)
                    .HasColumnName("elementContentMirrored")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementDevice).HasColumnName("elementDevice");

                entity.Property(e => e.ElementInstance)
                    .HasColumnName("elementInstance")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMaxX)
                    .HasColumnName("elementMountingMaxX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMaxY)
                    .HasColumnName("elementMountingMaxY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMaxZ)
                    .HasColumnName("elementMountingMaxZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMinX)
                    .HasColumnName("elementMountingMinX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMinY)
                    .HasColumnName("elementMountingMinY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementMountingMinZ)
                    .HasColumnName("elementMountingMinZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementProjectVersion).HasColumnName("elementProjectVersion");

                entity.Property(e => e.ElementType).HasColumnName("elementType");

                entity.HasOne(d => d.ElementProjectVersionNavigation)
                    .WithMany(p => p.TblElements)
                    .HasForeignKey(d => d.ElementProjectVersion)
                    .HasConstraintName("idx_tblElements_tblProjectVersions");
            });

            modelBuilder.Entity<TblElementComponent>(entity =>
            {
                entity.HasKey(e => e.ComponentId)
                    .HasName("idx_tblElementComponents_primaryKey");

                entity.ToTable("tblElementComponents");

                entity.HasIndex(e => new { e.ComponentId, e.ComponentElement }, "idx_tblElementComponents_componentId_componentElement");

                entity.Property(e => e.ComponentId).HasColumnName("componentId");

                entity.Property(e => e.ComponentElement).HasColumnName("componentElement");

                entity.Property(e => e.ComponentFileName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("componentFileName");

                entity.Property(e => e.ComponentLocationMaxX).HasColumnName("componentLocationMaxX");

                entity.Property(e => e.ComponentLocationMaxY).HasColumnName("componentLocationMaxY");

                entity.Property(e => e.ComponentLocationMinX).HasColumnName("componentLocationMinX");

                entity.Property(e => e.ComponentLocationMinY).HasColumnName("componentLocationMinY");

                entity.Property(e => e.ComponentSchemeType).HasColumnName("componentSchemeType");

                entity.Property(e => e.ComponentTransformationAngle).HasColumnName("componentTransformationAngle");

                entity.Property(e => e.ComponentTransformationHandleX).HasColumnName("componentTransformationHandleX");

                entity.Property(e => e.ComponentTransformationHandleY).HasColumnName("componentTransformationHandleY");

                entity.Property(e => e.ComponentTransformationPositionX).HasColumnName("componentTransformationPositionX");

                entity.Property(e => e.ComponentTransformationPositionY).HasColumnName("componentTransformationPositionY");

                entity.HasOne(d => d.ComponentElementNavigation)
                    .WithMany(p => p.TblElementComponents)
                    .HasForeignKey(d => d.ComponentElement)
                    .HasConstraintName("idx_tblElementComponents_tblElements");
            });

            modelBuilder.Entity<TblElementComponentInstance>(entity =>
            {
                entity.HasKey(e => e.ComponentInstanceId)
                    .HasName("idx_tblElementComponentInstances_primaryKey");

                entity.ToTable("tblElementComponentInstances");

                entity.HasIndex(e => new { e.ComponentInstanceId, e.ComponentInstanceElementInstance }, "idx_tblElementComponentInstances_componentInstanceId_componentInstanceElementInstance");

                entity.Property(e => e.ComponentInstanceId).HasColumnName("componentInstanceId");

                entity.Property(e => e.ComponentInstanceComponent).HasColumnName("componentInstanceComponent");

                entity.Property(e => e.ComponentInstanceElementInstance).HasColumnName("componentInstanceElementInstance");

                entity.Property(e => e.ComponentInstanceInstance).HasColumnName("componentInstanceInstance");

                entity.Property(e => e.ComponentInstanceSignature)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("componentInstanceSignature");

                entity.Property(e => e.ComponentInstanceTransformationAngle).HasColumnName("componentInstanceTransformationAngle");

                entity.Property(e => e.ComponentInstanceTransformationHandleX).HasColumnName("componentInstanceTransformationHandleX");

                entity.Property(e => e.ComponentInstanceTransformationHandleY).HasColumnName("componentInstanceTransformationHandleY");

                entity.Property(e => e.ComponentInstanceTransformationPositionX).HasColumnName("componentInstanceTransformationPositionX");

                entity.Property(e => e.ComponentInstanceTransformationPositionY).HasColumnName("componentInstanceTransformationPositionY");

                entity.Property(e => e.ComponentInstanceUsed).HasColumnName("componentInstanceUsed");

                entity.HasOne(d => d.ComponentInstanceElementInstanceNavigation)
                    .WithMany(p => p.TblElementComponentInstances)
                    .HasForeignKey(d => d.ComponentInstanceElementInstance)
                    .HasConstraintName("idx_tblElementComponentInstances_tblElementInstances");
            });

            modelBuilder.Entity<TblElementConnector>(entity =>
            {
                entity.HasKey(e => e.ConnectorId)
                    .HasName("idx_tblElementConnectors_primaryKey");

                entity.ToTable("tblElementConnectors");

                entity.HasIndex(e => new { e.ConnectorId, e.ConnectorComponent }, "idx_tblElementConnectors_connectorId_connectorComponent");

                entity.Property(e => e.ConnectorId).HasColumnName("connectorId");

                entity.Property(e => e.ConnectorCompatibility).HasColumnName("connectorCompatibility");

                entity.Property(e => e.ConnectorComponent).HasColumnName("connectorComponent");

                entity.Property(e => e.ConnectorDirection).HasColumnName("connectorDirection");

                entity.Property(e => e.ConnectorLabelContent)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("connectorLabelContent");

                entity.Property(e => e.ConnectorLabelDirection).HasColumnName("connectorLabelDirection");

                entity.Property(e => e.ConnectorLabelOrientation).HasColumnName("connectorLabelOrientation");

                entity.Property(e => e.ConnectorPositionLabelX).HasColumnName("connectorPositionLabelX");

                entity.Property(e => e.ConnectorPositionLabelY).HasColumnName("connectorPositionLabelY");

                entity.Property(e => e.ConnectorPositionX).HasColumnName("connectorPositionX");

                entity.Property(e => e.ConnectorPositionY).HasColumnName("connectorPositionY");

                entity.Property(e => e.ConnectorType).HasColumnName("connectorType");

                entity.HasOne(d => d.ConnectorComponentNavigation)
                    .WithMany(p => p.TblElementConnectors)
                    .HasForeignKey(d => d.ConnectorComponent)
                    .HasConstraintName("idx_tblElementConnectors_tblElementComponents");
            });

            modelBuilder.Entity<TblElementConnectorInstance>(entity =>
            {
                entity.HasKey(e => e.ConnectorInstanceId)
                    .HasName("idx_tblElementConnectorInstances_primaryKey");

                entity.ToTable("tblElementConnectorInstances");

                entity.HasIndex(e => new { e.ConnectorInstanceId, e.ConnectorInstanceComponentInstance }, "idx_tblElementConnectorInstances_connectorInstanceId_connectorInstanceComponentInstance");

                entity.Property(e => e.ConnectorInstanceId).HasColumnName("connectorInstanceId");

                entity.Property(e => e.ConnectorInstanceComponentInstance).HasColumnName("connectorInstanceComponentInstance");

                entity.Property(e => e.ConnectorInstanceConnector).HasColumnName("connectorInstanceConnector");

                entity.Property(e => e.ConnectorInstanceInstance).HasColumnName("connectorInstanceInstance");

                entity.HasOne(d => d.ConnectorInstanceComponentInstanceNavigation)
                    .WithMany(p => p.TblElementConnectorInstances)
                    .HasForeignKey(d => d.ConnectorInstanceComponentInstance)
                    .HasConstraintName("idx_tblElementConnectorInstances_tblElementComponentInstances");
            });

            modelBuilder.Entity<TblElementInstance>(entity =>
            {
                entity.HasKey(e => e.ElementInstanceId)
                    .HasName("idx_tblElementInstances_primaryKey");

                entity.ToTable("tblElementInstances");

                entity.HasIndex(e => new { e.ElementInstanceId, e.ElementInstanceProjectVersion }, "idx_tblElementInstances_elementInstanceId_elementInstanceProjectVersion");

                entity.HasIndex(e => new { e.ElementInstanceProjectVersion, e.ElementInstanceActual }, "idx_tblElementInstances_elementInstanceProjectVersion_elementInstanceActual");

                entity.Property(e => e.ElementInstanceId).HasColumnName("elementInstanceId");

                entity.Property(e => e.ElementInstanceAccessoryQuantity)
                    .HasColumnName("elementInstanceAccessoryQuantity")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceActual).HasColumnName("elementInstanceActual");

                entity.Property(e => e.ElementInstanceAngleXy)
                    .HasColumnName("elementInstanceAngleXY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceAngleXz)
                    .HasColumnName("elementInstanceAngleXZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceAngleYz)
                    .HasColumnName("elementInstanceAngleYZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceElement).HasColumnName("elementInstanceElement");

                entity.Property(e => e.ElementInstanceGroup).HasColumnName("elementInstanceGroup");

                entity.Property(e => e.ElementInstanceHandleX)
                    .HasColumnName("elementInstanceHandleX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceHandleY)
                    .HasColumnName("elementInstanceHandleY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceHandleZ)
                    .HasColumnName("elementInstanceHandleZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceInstance).HasColumnName("elementInstanceInstance");

                entity.Property(e => e.ElementInstanceParent)
                    .HasColumnName("elementInstanceParent")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstancePositionX)
                    .HasColumnName("elementInstancePositionX")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstancePositionY)
                    .HasColumnName("elementInstancePositionY")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstancePositionZ)
                    .HasColumnName("elementInstancePositionZ")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ElementInstanceProjectVersion).HasColumnName("elementInstanceProjectVersion");

                entity.Property(e => e.ElementInstanceSection).HasColumnName("elementInstanceSection");

                entity.Property(e => e.ElementInstanceSignature)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("elementInstanceSignature");

                entity.Property(e => e.ElementInstanceSignatureAuto).HasColumnName("elementInstanceSignatureAuto");

                entity.HasOne(d => d.ElementInstanceElementNavigation)
                    .WithMany(p => p.TblElementInstances)
                    .HasForeignKey(d => d.ElementInstanceElement)
                    .HasConstraintName("idx_tblElementInstances_tblElements");
            });

            modelBuilder.Entity<TblField>(entity =>
            {
                entity.HasKey(e => e.FieldId)
                    .HasName("idx_tblFields_primaryKey");

                entity.ToTable("tblFields");

                entity.HasIndex(e => e.FieldTable, "idx_tblFields_fieldTable");

                entity.Property(e => e.FieldId).HasColumnName("fieldId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FieldMax).HasColumnName("fieldMax");

                entity.Property(e => e.FieldMin).HasColumnName("fieldMin");

                entity.Property(e => e.FieldName)
                    .HasMaxLength(255)
                    .HasColumnName("fieldName");

                entity.Property(e => e.FieldOrder).HasColumnName("fieldOrder");

                entity.Property(e => e.FieldPublic).HasColumnName("fieldPublic");

                entity.Property(e => e.FieldReference).HasColumnName("fieldReference");

                entity.Property(e => e.FieldSize).HasColumnName("fieldSize");

                entity.Property(e => e.FieldStructure).HasColumnName("fieldStructure");

                entity.Property(e => e.FieldTable).HasColumnName("fieldTable");

                entity.Property(e => e.FieldType)
                    .HasMaxLength(50)
                    .HasColumnName("fieldType");

                entity.HasOne(d => d.FieldTableNavigation)
                    .WithMany(p => p.TblFields)
                    .HasForeignKey(d => d.FieldTable)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblFields_tblTables");
            });

            modelBuilder.Entity<TblGroup>(entity =>
            {
                entity.HasKey(e => e.GroupId)
                    .HasName("idx_tblGroups_primaryKey");

                entity.ToTable("tblGroups");

                entity.Property(e => e.GroupId).HasColumnName("groupId");

                entity.Property(e => e.GroupName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("groupName");

                entity.Property(e => e.GroupParent).HasColumnName("groupParent");

                entity.Property(e => e.GroupProjectVersion).HasColumnName("groupProjectVersion");

                entity.HasOne(d => d.GroupProjectVersionNavigation)
                    .WithMany(p => p.TblGroups)
                    .HasForeignKey(d => d.GroupProjectVersion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblGroups_tblProjectVersions");
            });

            modelBuilder.Entity<TblHistoryJointRelation>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblHistoryJointRelations");

                entity.Property(e => e.HistoryJointFirst).HasColumnName("historyJointFirst");

                entity.Property(e => e.HistoryJointSecond).HasColumnName("historyJointSecond");

                entity.Property(e => e.HistoryOperation).HasColumnName("historyOperation");

                entity.Property(e => e.HistoryTime)
                    .HasColumnType("datetime")
                    .HasColumnName("historyTime");
            });

            modelBuilder.Entity<TblLink>(entity =>
            {
                entity.HasKey(e => e.LinkId)
                    .HasName("idx_tblLinks_primaryKey");

                entity.ToTable("tblLinks");

                entity.HasIndex(e => new { e.LinkId, e.LinkScheme }, "idx_tblLinks_linkId_linkScheme");

                entity.HasIndex(e => new { e.LinkProjectVersion, e.LinkActual }, "idx_tblLinks_linkProjectVersion_linkActual");

                entity.Property(e => e.LinkId).HasColumnName("linkId");

                entity.Property(e => e.LinkActual).HasColumnName("linkActual");

                entity.Property(e => e.LinkConnectorInstanceFrom).HasColumnName("linkConnectorInstanceFrom");

                entity.Property(e => e.LinkConnectorInstanceTo).HasColumnName("linkConnectorInstanceTo");

                entity.Property(e => e.LinkGroup).HasColumnName("linkGroup");

                entity.Property(e => e.LinkInstance).HasColumnName("linkInstance");

                entity.Property(e => e.LinkLabel)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("linkLabel");

                entity.Property(e => e.LinkProjectVersion).HasColumnName("linkProjectVersion");

                entity.Property(e => e.LinkScheme).HasColumnName("linkScheme");

                entity.HasOne(d => d.LinkSchemeNavigation)
                    .WithMany(p => p.TblLinks)
                    .HasForeignKey(d => d.LinkScheme)
                    .HasConstraintName("idx_tblLinks_tblSchemes");
            });

            modelBuilder.Entity<TblLinkGroup>(entity =>
            {
                entity.HasKey(e => e.LinkGroupId)
                    .HasName("idx_tblLinkGroups_primaryKey");

                entity.ToTable("tblLinkGroups");

                entity.HasIndex(e => new { e.LinkGroupId, e.LinkGroupScheme }, "idx_tblLinkGroups_linkGroupId_linkGroupScheme");

                entity.HasIndex(e => new { e.LinkGroupProjectVersion, e.LinkGroupActual }, "idx_tblLinkGroups_linkGroupProjectVersion_linkGroupActual");

                entity.Property(e => e.LinkGroupId).HasColumnName("linkGroupId");

                entity.Property(e => e.LinkGroupActual).HasColumnName("linkGroupActual");

                entity.Property(e => e.LinkGroupInstance).HasColumnName("linkGroupInstance");

                entity.Property(e => e.LinkGroupLabel)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("linkGroupLabel");

                entity.Property(e => e.LinkGroupProjectVersion).HasColumnName("linkGroupProjectVersion");

                entity.Property(e => e.LinkGroupScheme).HasColumnName("linkGroupScheme");

                entity.HasOne(d => d.LinkGroupSchemeNavigation)
                    .WithMany(p => p.TblLinkGroups)
                    .HasForeignKey(d => d.LinkGroupScheme)
                    .HasConstraintName("idx_tblLinkGroups_tblSchemes");
            });

            modelBuilder.Entity<TblLinkVertex>(entity =>
            {
                entity.HasKey(e => e.VertexId)
                    .HasName("idx_tblLinkVertexes_primaryKey");

                entity.ToTable("tblLinkVertexes");

                entity.HasIndex(e => new { e.VertexId, e.VertexLink }, "idx_tblLinkVertexes_vertexId_vertexLink");

                entity.HasIndex(e => new { e.VertexProjectVersion, e.VertexActual }, "idx_tblLinkVertexes_vertexProjectVersion_vertexActual");

                entity.Property(e => e.VertexId).HasColumnName("vertexId");

                entity.Property(e => e.VertexActual).HasColumnName("vertexActual");

                entity.Property(e => e.VertexLink).HasColumnName("vertexLink");

                entity.Property(e => e.VertexPositionX).HasColumnName("vertexPositionX");

                entity.Property(e => e.VertexPositionY).HasColumnName("vertexPositionY");

                entity.Property(e => e.VertexProjectVersion).HasColumnName("vertexProjectVersion");

                entity.HasOne(d => d.VertexLinkNavigation)
                    .WithMany(p => p.TblLinkVertices)
                    .HasForeignKey(d => d.VertexLink)
                    .HasConstraintName("idx_tblLinkVertexes_tblLinks");
            });

            modelBuilder.Entity<TblLock>(entity =>
            {
                entity.HasKey(e => e.LockId)
                    .HasName("idx_tblLocks_primaryKey");

                entity.ToTable("tblLocks");

                entity.HasIndex(e => e.LockRecord, "idx_tblLocks_lockRecord")
                    .IsUnique();

                entity.Property(e => e.LockId).HasColumnName("lockId");

                entity.Property(e => e.LockRecord).HasColumnName("lockRecord");

                entity.Property(e => e.LockTime)
                    .HasColumnType("datetime")
                    .HasColumnName("lockTime");

                entity.Property(e => e.LockUser).HasColumnName("lockUser");
            });

            modelBuilder.Entity<TblOptimizationCommand>(entity =>
            {
                entity.HasKey(e => e.Command)
                    .HasName("idx_tblOptimizationCommand_primaryKey");

                entity.ToTable("tblOptimizationCommand");

                entity.Property(e => e.Command).HasMaxLength(64);
            });

            modelBuilder.Entity<TblProject>(entity =>
            {
                entity.HasKey(e => e.ProjectId)
                    .HasName("idx_tblProjects_primaryKey");

                entity.ToTable("tblProjects");

                entity.HasIndex(e => e.TblRelativeAssemblyNavigationId, "IX_TblProjects_TblRelativeAssemblyNavigationId");

                entity.Property(e => e.ProjectId).HasColumnName("projectId");

                entity.Property(e => e.ProjectAmperageShortCircuit).HasColumnName("projectAmperageShortCircuit");

                entity.Property(e => e.ProjectApprover)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("projectApprover");

                entity.Property(e => e.ProjectCabinetService).HasColumnName("projectCabinetService");

                entity.Property(e => e.ProjectCustomer)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("projectCustomer");

                entity.Property(e => e.ProjectDeveloperShort)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("projectDeveloperShort");

                entity.Property(e => e.ProjectDimensionsHeight).HasColumnName("projectDimensionsHeight");

                entity.Property(e => e.ProjectGaugePe).HasColumnName("ProjectGaugePE");

                entity.Property(e => e.ProjectIp)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("projectIP");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("projectName");

                entity.Property(e => e.ProjectReleaseYear).HasColumnName("projectReleaseYear");

                entity.Property(e => e.ProjectRemark)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("projectRemark");

                entity.Property(e => e.ProjectSectioning)
                    .HasMaxLength(2)
                    .IsUnicode(false)
                    .HasColumnName("projectSectioning");

                entity.Property(e => e.ProjectSimultaneity).HasColumnName("projectSimultaneity");

                entity.Property(e => e.ProjectStage)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("projectStage");

                entity.Property(e => e.ProjectTask)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("projectTask");

                entity.HasOne(d => d.TblRelativeAssemblyNavigation)
                    .WithMany(p => p.TblProjects)
                    .HasForeignKey(d => d.TblRelativeAssemblyNavigationId)
                    .HasConstraintName("FK_TblProjects_TblRelativeAssemblies_TblRelativeAssemblyNavigationId");
            });

            modelBuilder.Entity<TblProjectRelation>(entity =>
            {
                entity.HasIndex(e => e.ArrayId, "IX_TblProjectRelations_ArrayId");

                entity.HasIndex(e => e.TblRelativeAssemblyId, "IX_TblProjectRelations_TblRelativeAssemblyId");

                entity.HasOne(d => d.Array)
                    .WithMany(p => p.TblProjectRelations)
                    .HasForeignKey(d => d.ArrayId);

                entity.HasOne(d => d.TblRelativeAssembly)
                    .WithMany(p => p.TblProjectRelations)
                    .HasForeignKey(d => d.TblRelativeAssemblyId);
            });

            modelBuilder.Entity<TblProjectVersion>(entity =>
            {
                entity.HasKey(e => e.ProjectVersionId)
                    .HasName("idx_tblProjectVersions_primaryKey");

                entity.ToTable("tblProjectVersions");

                entity.HasIndex(e => new { e.ProjectVersionId, e.ProjectVersionProject }, "idx_tblProjectVersions_projectVersionId_projectVersionProject");

                entity.Property(e => e.ProjectVersionId).HasColumnName("projectVersionId");

                entity.Property(e => e.ProjectVersionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("projectVersionDate");

                entity.Property(e => e.ProjectVersionManualChanged)
                    .HasColumnName("projectVersionManualChanged")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ProjectVersionNumber).HasColumnName("projectVersionNumber");

                entity.Property(e => e.ProjectVersionProject).HasColumnName("projectVersionProject");

                entity.Property(e => e.ProjectVersionRemark)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("projectVersionRemark");

                entity.Property(e => e.ProjectVersionTemporary).HasColumnName("projectVersionTemporary");

                entity.Property(e => e.ProjectVersionUser).HasColumnName("projectVersionUser");

                entity.Property(e => e.ProjectVersionUserName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("projectVersionUserName");

                entity.HasOne(d => d.ProjectVersionProjectNavigation)
                    .WithMany(p => p.TblProjectVersions)
                    .HasForeignKey(d => d.ProjectVersionProject)
                    .HasConstraintName("idx_tblProjectVersions_tblProjects");
            });

            modelBuilder.Entity<TblProperty>(entity =>
            {
                entity.HasKey(e => e.PropertyId)
                    .HasName("idx_tblProperties_primaryKey");

                entity.ToTable("tblProperties");

                entity.HasIndex(e => new { e.PropertyProjectVersion, e.PropertyActual }, "idx_tblProperties_propertyProjectVersion_propertyActual");

                entity.HasIndex(e => new { e.PropertyProjectVersion, e.PropertyProperty }, "idx_tblProperties_propertyProjectVersion_propertyProperty");

                entity.Property(e => e.PropertyId).HasColumnName("propertyId");

                entity.Property(e => e.PropertyActual).HasColumnName("propertyActual");

                entity.Property(e => e.PropertyName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("propertyName");

                entity.Property(e => e.PropertyProjectVersion).HasColumnName("propertyProjectVersion");

                entity.Property(e => e.PropertyProperty).HasColumnName("propertyProperty");

                entity.HasOne(d => d.PropertyProjectVersionNavigation)
                    .WithMany(p => p.TblProperties)
                    .HasForeignKey(d => d.PropertyProjectVersion)
                    .HasConstraintName("idx_tblProperties_tblProjectVersions");
            });

            modelBuilder.Entity<TblPropertyValue>(entity =>
            {
                entity.HasKey(e => e.PropertyValueId)
                    .HasName("idx_tblPropertyValues_primaryKey");

                entity.ToTable("tblPropertyValues");

                entity.HasIndex(e => new { e.PropertyValueProjectVersion, e.PropertyValueActual }, "idx_tblPropertyValues_propertyValueProjectVersion_propertyValueActual");

                entity.HasIndex(e => new { e.PropertyValueProjectVersion, e.PropertyValueInstance }, "idx_tblPropertyValues_propertyValueProjectVersion_propertyValueInstance");

                entity.HasIndex(e => new { e.PropertyValueProjectVersion, e.PropertyValueProperty }, "idx_tblPropertyValues_propertyValueProjectVersion_propertyValueProperty");

                entity.Property(e => e.PropertyValueId).HasColumnName("propertyValueId");

                entity.Property(e => e.PropertyValueActual).HasColumnName("propertyValueActual");

                entity.Property(e => e.PropertyValueContent)
                    .HasColumnType("sql_variant")
                    .HasColumnName("propertyValueContent");

                entity.Property(e => e.PropertyValueInstance).HasColumnName("propertyValueInstance");

                entity.Property(e => e.PropertyValueProjectVersion).HasColumnName("propertyValueProjectVersion");

                entity.Property(e => e.PropertyValueProperty).HasColumnName("propertyValueProperty");

                entity.HasOne(d => d.PropertyValueProjectVersionNavigation)
                    .WithMany(p => p.TblPropertyValues)
                    .HasForeignKey(d => d.PropertyValueProjectVersion)
                    .HasConstraintName("idx_tblPropertyValues_tblProjectVersions");
            });

            modelBuilder.Entity<TblRecord>(entity =>
            {
                entity.HasKey(e => e.RecordId)
                    .HasName("idx_tblRecords_primaryKey");

                entity.ToTable("tblRecords");

                entity.HasIndex(e => e.RecordTable, "idx_tblRecords_recordTable");

                entity.Property(e => e.RecordId).HasColumnName("recordId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.RecordTable).HasColumnName("recordTable");

                entity.HasOne(d => d.RecordTableNavigation)
                    .WithMany(p => p.TblRecords)
                    .HasForeignKey(d => d.RecordTable)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblRecords_tblTables");
            });

            modelBuilder.Entity<TblScheme>(entity =>
            {
                entity.HasKey(e => e.SchemeId)
                    .HasName("idx_tblSchemes_primaryKey");

                entity.ToTable("tblSchemes");

                entity.Property(e => e.SchemeId).HasColumnName("schemeId");

                entity.Property(e => e.SchemeProjectVersion).HasColumnName("schemeProjectVersion");

                entity.Property(e => e.SchemeType).HasColumnName("schemeType");

                entity.HasOne(d => d.SchemeProjectVersionNavigation)
                    .WithMany(p => p.TblSchemes)
                    .HasForeignKey(d => d.SchemeProjectVersion)
                    .HasConstraintName("idx_tblSchemes_tblProjectVersions");
            });

            modelBuilder.Entity<TblSection>(entity =>
            {
                entity.HasKey(e => e.SectionId)
                    .HasName("idx_tblSections_primaryKey");

                entity.ToTable("tblSections");

                entity.Property(e => e.SectionId).HasColumnName("sectionId");

                entity.Property(e => e.SectionActual).HasColumnName("sectionActual");

                entity.Property(e => e.SectionColor).HasColumnName("sectionColor");

                entity.Property(e => e.SectionName)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("sectionName");

                entity.Property(e => e.SectionParent).HasColumnName("sectionParent");

                entity.Property(e => e.SectionProjectVersion).HasColumnName("sectionProjectVersion");

                entity.HasOne(d => d.SectionProjectVersionNavigation)
                    .WithMany(p => p.TblSections)
                    .HasForeignKey(d => d.SectionProjectVersion)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblSections_tblProjectVersions");
            });

            modelBuilder.Entity<TblSpecification>(entity =>
            {
                entity.HasKey(e => e.SpecificationId)
                    .HasName("idx_tblSpecification_primaryKey");

                entity.HasIndex(e => e.SpecificationCode, "idx_tblSpecification_specificationCode");

                entity.Property(e => e.SpecificationId).HasColumnName("specificationId");

                entity.Property(e => e.SpecificationCode)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .HasColumnName("specificationCode");

                entity.Property(e => e.SpecificationDelivery).HasColumnName("specificationDelivery");

                entity.Property(e => e.SpecificationMeasure)
                    .HasMaxLength(32)
                    .IsUnicode(false)
                    .HasColumnName("specificationMeasure");

                entity.Property(e => e.SpecificationName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("specificationName");

                entity.Property(e => e.SpecificationPrice).HasColumnName("specificationPrice");

                entity.Property(e => e.SpecificationRemainder).HasColumnName("specificationRemainder");

                entity.Property(e => e.SpecificationRemark)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("specificationRemark");
            });

            modelBuilder.Entity<TblStructure>(entity =>
            {
                entity.HasKey(e => e.StructureId)
                    .HasName("idx_tblStructure_primaryKey");

                entity.ToTable("tblStructure");

                entity.Property(e => e.StructureId).HasColumnName("structureId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.StructureDate)
                    .HasColumnType("datetime")
                    .HasColumnName("structureDate");

                entity.Property(e => e.StructureUser).HasColumnName("structureUser");
            });

            modelBuilder.Entity<TblTable>(entity =>
            {
                entity.HasKey(e => e.TableId)
                    .HasName("idx_tblTables_primaryKey");

                entity.ToTable("tblTables");

                entity.HasIndex(e => e.TableCluster, "idx_tblTables_tableCluster");

                entity.Property(e => e.TableId).HasColumnName("tableId");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.TableCluster).HasColumnName("tableCluster");

                entity.Property(e => e.TableName)
                    .HasMaxLength(255)
                    .HasColumnName("tableName");

                entity.Property(e => e.TableStructure).HasColumnName("tableStructure");

                entity.HasOne(d => d.TableClusterNavigation)
                    .WithMany(p => p.TblTables)
                    .HasForeignKey(d => d.TableCluster)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblTables_tblClusters");
            });

            modelBuilder.Entity<TblTreePath>(entity =>
            {
                entity.ToTable("TblTreePath");
            });

            modelBuilder.Entity<TblTrunk>(entity =>
            {
                entity.HasKey(e => e.TrunkId)
                    .HasName("idx_tblTrunks_primaryKey");

                entity.ToTable("tblTrunks");

                entity.HasIndex(e => new { e.TrunkId, e.TrunkProjectVersion }, "idx_tblTrunks_trunkId_trunkProjectVersion");

                entity.HasIndex(e => new { e.TrunkProjectVersion, e.TrunkActual }, "idx_tblTrunks_trunkProjectVersion_trunkActual");

                entity.HasIndex(e => new { e.TrunkScheme, e.TrunkId }, "idx_tblTrunks_trunkScheme_trunkId");

                entity.Property(e => e.TrunkId).HasColumnName("trunkId");

                entity.Property(e => e.TrunkActual).HasColumnName("trunkActual");

                entity.Property(e => e.TrunkInstance).HasColumnName("trunkInstance");

                entity.Property(e => e.TrunkProjectVersion).HasColumnName("trunkProjectVersion");

                entity.Property(e => e.TrunkScheme).HasColumnName("trunkScheme");

                entity.Property(e => e.TrunkTransformationAngle).HasColumnName("trunkTransformationAngle");

                entity.Property(e => e.TrunkTransformationHandleX).HasColumnName("trunkTransformationHandleX");

                entity.Property(e => e.TrunkTransformationHandleY).HasColumnName("trunkTransformationHandleY");

                entity.Property(e => e.TrunkTransformationPositionX).HasColumnName("trunkTransformationPositionX");

                entity.Property(e => e.TrunkTransformationPositionY).HasColumnName("trunkTransformationPositionY");

                entity.HasOne(d => d.TrunkProjectVersionNavigation)
                    .WithMany(p => p.TblTrunks)
                    .HasForeignKey(d => d.TrunkProjectVersion)
                    .HasConstraintName("idx_tblTrunks_tblProjectVersions");
            });

            modelBuilder.Entity<TblTrunkBinding>(entity =>
            {
                entity.HasKey(e => e.BindingId)
                    .HasName("idx_tblTrunkBindings_primaryKey");

                entity.ToTable("tblTrunkBindings");

                entity.HasIndex(e => new { e.BindingId, e.BindingTrunk }, "idx_tblTrunkBindings_bindingId_bindingTrunk");

                entity.HasIndex(e => new { e.BindingProjectVersion, e.BindingActual }, "idx_tblTrunkBindings_bindingProjectVersion_bindingActual");

                entity.HasIndex(e => e.BindingTrunk, "idx_tblTrunkBindings_bindingTrunk");

                entity.Property(e => e.BindingId).HasColumnName("bindingId");

                entity.Property(e => e.BindingActual).HasColumnName("bindingActual");

                entity.Property(e => e.BindingConnector).HasColumnName("bindingConnector");

                entity.Property(e => e.BindingInstance).HasColumnName("bindingInstance");

                entity.Property(e => e.BindingProjectVersion).HasColumnName("bindingProjectVersion");

                entity.Property(e => e.BindingTrunk).HasColumnName("bindingTrunk");

                entity.HasOne(d => d.BindingTrunkNavigation)
                    .WithMany(p => p.TblTrunkBindings)
                    .HasForeignKey(d => d.BindingTrunk)
                    .HasConstraintName("idx_tblTrunkBindings_tblTrunks");
            });

            modelBuilder.Entity<TblTypeDevice>(entity =>
            {
                entity.ToTable("TblTypeDevice");
            });

            modelBuilder.Entity<TblUserDeviceParameter>(entity =>
            {
                entity.HasKey(e => e.DeviceParameterId);

                entity.HasIndex(e => e.DeviceParameterTable, "IX_TblUserDeviceParameters_DeviceParameterTable");

                entity.HasOne(d => d.DeviceParameterTableNavigation)
                    .WithMany(p => p.TblUserDeviceParameters)
                    .HasForeignKey(d => d.DeviceParameterTable)
                    .HasConstraintName("FK_TblUserDeviceParameters_TblTables_DeviceParameterTable");
            });

            modelBuilder.Entity<TblValue>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tblValues");

                entity.HasIndex(e => e.ValueRecord, "IX_tblValues_tblRecords_valueRecord");

                entity.HasIndex(e => e.ValueTable, "IX_tblValues_tblTables_valueTable");

                entity.HasIndex(e => e.ValueField, "idx_tblValues_valueField");

                entity.HasIndex(e => e.ValueRecord, "idx_tblValues_valueRecord");

                entity.HasIndex(e => new { e.ValueTable, e.ValueRecord, e.ValueField }, "idx_tblValues_valueTable_valueRecord_valueField");

                entity.Property(e => e.EntityKey).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ValueData)
                    .HasColumnType("sql_variant")
                    .HasColumnName("valueData");

                entity.Property(e => e.ValueField).HasColumnName("valueField");

                entity.Property(e => e.ValueId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("valueId");

                entity.Property(e => e.ValueInvisible)
                    .HasColumnName("valueInvisible")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.ValueRecord).HasColumnName("valueRecord");

                entity.Property(e => e.ValueReference).HasColumnName("valueReference");

                entity.Property(e => e.ValueTable).HasColumnName("valueTable");

                entity.HasOne(d => d.ValueFieldNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ValueField)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("idx_tblValues_tblFields");

                entity.HasOne(d => d.ValueRecordNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ValueRecord);

                entity.HasOne(d => d.ValueReferenceNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ValueReference);

                entity.HasOne(d => d.ValueTableNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.ValueTable);
            });

            modelBuilder.Entity<TblWire>(entity =>
            {
                entity.HasKey(e => e.WireId)
                    .HasName("idx_tblWires_primaryKey");

                entity.ToTable("tblWires");

                entity.HasIndex(e => new { e.WireId, e.WireProjectVersion }, "idx_tblWires_wireId_wireProjectVersion");

                entity.HasIndex(e => new { e.WireProjectVersion, e.WireActual }, "idx_tblWires_wireProjectVersion_wireActual");

                entity.Property(e => e.WireId).HasColumnName("wireId");

                entity.Property(e => e.WireActual).HasColumnName("wireActual");

                entity.Property(e => e.WireDiameter).HasColumnName("wireDiameter");

                entity.Property(e => e.WireEquipment).HasColumnName("wireEquipment");

                entity.Property(e => e.WireGause).HasColumnName("wireGause");

                entity.Property(e => e.WireHeight).HasColumnName("wireHeight");

                entity.Property(e => e.WireInstance).HasColumnName("wireInstance");

                entity.Property(e => e.WirePhase).HasColumnName("wirePhase");

                entity.Property(e => e.WireProjectVersion).HasColumnName("wireProjectVersion");

                entity.Property(e => e.WireType).HasColumnName("wireType");

                entity.Property(e => e.WireWidth).HasColumnName("wireWidth");

                entity.HasOne(d => d.WireProjectVersionNavigation)
                    .WithMany(p => p.TblWires)
                    .HasForeignKey(d => d.WireProjectVersion)
                    .HasConstraintName("idx_tblWires_tblProjectVersions");
            });

            modelBuilder.Entity<TblWireVertex>(entity =>
            {
                entity.HasKey(e => e.VertexId)
                    .HasName("idx_tblWireVertexes_primaryKey");

                entity.ToTable("tblWireVertexes");

                entity.HasIndex(e => e.VertexWire, "idx_tblWireVertexes_vertexWire");

                entity.Property(e => e.VertexId).HasColumnName("vertexId");

                entity.Property(e => e.VertexWire).HasColumnName("vertexWire");

                entity.Property(e => e.VertexX).HasColumnName("vertexX");

                entity.Property(e => e.VertexY).HasColumnName("vertexY");

                entity.Property(e => e.VertexZ).HasColumnName("vertexZ");

                entity.HasOne(d => d.VertexWireNavigation)
                    .WithMany(p => p.TblWireVertices)
                    .HasForeignKey(d => d.VertexWire)
                    .HasConstraintName("idx_tblWireVertexes_tblWires");
            });

            modelBuilder.Entity<TempChangedDatum>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tempChangedData");

                entity.Property(e => e.SessionId).HasColumnName("sessionId");

                entity.Property(e => e.ValueData)
                    .HasColumnType("sql_variant")
                    .HasColumnName("valueData");

                entity.Property(e => e.ValueId).HasColumnName("valueId");
            });

            modelBuilder.Entity<TempDataTable>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("tempDataTable");

                entity.Property(e => e.SessionId).HasColumnName("sessionId");

                entity.Property(e => e.TempRowId).HasColumnName("tempRowId");

                entity.Property(e => e.ValueData)
                    .HasColumnType("sql_variant")
                    .HasColumnName("valueData");

                entity.Property(e => e.ValueField).HasColumnName("valueField");

                entity.Property(e => e.ValueTable).HasColumnName("valueTable");
            });

            modelBuilder.Entity<ViewProjectVersion>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("viewProjectVersions");

                entity.Property(e => e.ProjectName)
                    .HasMaxLength(138)
                    .IsUnicode(false)
                    .HasColumnName("projectName");

                entity.Property(e => e.ProjectVersionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("projectVersionDate");

                entity.Property(e => e.ProjectVersionId).HasColumnName("projectVersionId");

                entity.Property(e => e.ProjectVersionNumber).HasColumnName("projectVersionNumber");

                entity.Property(e => e.ProjectVersionProject).HasColumnName("projectVersionProject");

                entity.Property(e => e.ProjectVersionRemark)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("projectVersionRemark");

                entity.Property(e => e.ProjectVersionUser).HasColumnName("projectVersionUser");

                entity.Property(e => e.ProjectVersionUserName)
                    .HasMaxLength(256)
                    .IsUnicode(false)
                    .HasColumnName("projectVersionUserName");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
