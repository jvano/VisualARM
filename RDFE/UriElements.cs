using System;
using System.Collections.Generic;

namespace Vano.Tools.Azure.RDFE
{
    public class UriElements
    {
        public const string ServiceNamespace = "http://schemas.microsoft.com/windowsazure";

        public const string Failover = "failover";
        public const string Automorphism = "automorphism";
        public const string AutoRelocation = "autorelocation";
        public const string Plans = "plans";
        public const string Subscriptions = "subscriptions";
        public const string Web = "web";
        public const string WebAdmin = "webadmin";
        public const string Users = "users";
        public const string SourceControlKeys = "sourcecontrolkeys";
        public const string ResourceProvider = "resourceprovider";
        public const string MetricDefinitions = "metricdefinitions";
        public const string Authentication = "authentication";
        public const string GeoAdmin = "geoadmin";
        public const string HostingEnvironmentManagement = "hostingEnvironmentManagement";
        public const string GeoThrottling = "geothrottling";
        public const string DnsManagement = "dnsmanagement";
        public const string EventDiscovery = "eventdiscovery";
        public const string AccessControl = "accesscontrol";
        public const string Capacity = "capacities";
        public const string Offers = "offers";
        public const string Skus = "skus";
        public const string QuotaManagement = "quotamanagement";
        public const string ServerFarms = "serverfarms";
        public const string Usage = "usage";
        public const string Operations = "operations";
        public const string CustomProducts = "customproducts";
        public const string SiteStamps = "sitestamps";

        // Parameters
        public const string GetArmResourceIdForSiteNameParameters = "/DefaultSiteHostName/{defaultSiteHostName}/NamespaceDescriptor/{namespaceDescriptor}";
        public const string AddSiteName = "/AddSiteName";
        public const string DeleteSiteName = "/DeleteSiteName";
        public const string UpdateArmResourceIdForSiteName = "/UpdateArmResourceIdForSiteName";
        public const string OperationUpdateParameters = "/operationId/{operationId}/targetStatus/{targetStatus}";
        public const string NameTemplateParameter = "/{name}";
        public const string NameTemplateParameterName = "name";
        public const string HostnameTemplateParameter = "/{hostname}";
        public const string EventGridFilterNameParameterName = "eventGridFilterName";
        public const string SourceControlsTemplateParameter = "/{name}/sourcecontrols";
        public const string LocationStampTemplateParameter = "?locationName={locationName}&stampName={stampName}";
        public const string PublicStampsOnlyTemplateParameter = "?publicStampsOnly={publicStampsOnly}";
        public const string UserNameTemplateParameter = "/{userName}";
        public const string SourceControlTypeTemplateParameter = "/{sourceControlType}";
        public const string SourceControlEnvironmentTypeTemplateParameter = "?sourceControlEnvironmentType={sourceControlEnvironmentType}";
        public const string CertificateThumbprintTemplateParameter = "/{thumbprint}";
        public const string CsrPublicKeyHashTemplateParameter = "/{publicKeyHash}";
        public const string CommandTemplateParameter = "?comp=command";
        public const string UsersPublishAuthN = "?publishauthenticated&source={source}&protocol={protocol}&userAddress={userAddress}";
        public const string UsersPublishAuthZ = "?publishauthorized&userName={publishingUserName}&authorizedSite={siteName}&repository={isrepository}&source={source}&protocol={protocol}&userAddress={userAddress}";
        public const string UpdateSiteParameters = "?skipDnsRegistration={skipDnsRegistration}&skipCustomDomainVerification={skipCustomDomainVerification}&forceDnsRegistration={forceDnsRegistration}&ttlInSeconds={ttlInSeconds}";
        public const string DeleteSiteParameters = "?deleteMetrics={deleteMetrics}&deleteEmptyServerFarm={deleteEmptyServerFarm}&skipDnsRegistration={skipDnsRegistration}&deleteAllSlots={deleteAllSlots}&createSnapshot={createSnapshot}";
        public const string ExistsParameter = "/exists";
        public const string FunctionAppInfoParameter = "/functionappinfo";
        public const string MetricsParameters = "?names={metrics}&startTime={startTime}&endTime={endTime}&timeGrain={timeGrain}&details={details}&slotView={slotView}";
        public const string PerfMonParameters = "?names={names}&startTime={startTime}&endTime={endTime}&timeGrain={timeGrain}";
        public const string ServerFarmMetricsParameters = "?names={metrics}&startTime={startTime}&endTime={endTime}&timeGrain={timeGrain}&details={details}";
        public const string SystemMetricsParameters = "?names={metrics}&startTime={startTime}&endTime={endTime}&timeGrain={timeGrain}";
        public const string InstancesParameter = "&instances={instances}";
        public const string ComputeMode = "?computeMode={computeMode}&siteMode={siteMode}&enforcementScope={enforcementScope}";
        public const string PolicyParameters = "?computeMode={computeMode}&siteMode={siteMode}";
        public const string SkipValidationParameter = "?skipValidation={skipValidation}";
        public const string HybridConnectionEntityNameParameter = "/{entityName}";
        public const string PaginationParameters = "?pageNumber={pageNumber}&pageSize={pageSize}&filter={filter}";
        public const string ContinuationParameters = "?marker={marker}&recordCount={recordCount}";
        public const string ExtendableContinuationParameters = "&marker={marker}&recordCount={recordCount}";
        public const string UsersByTimeStampTemplateParameter = "?timeStamp={timeStamp}&count={count}";
        public const string AllowPendingStateParameter = "?allowPendingState={allowPendingState}";
        public const string AllowPendingStateParameterAndIgnoreQuotasAndCancelInProgressOperations = "?allowPendingState={allowPendingState}&ignoreQuotas={ignoreQuotas}&cancelInProgressOperations={cancelInProgressOperations}";
        public const string IgnoreQuotas = "?ignoreQuotas={ignoreQuotas}";
        public const string AsyncParameter = "?async={isAsync}";
        public const string AsyncBodyParameter = "/async";
        public const string PropertiesToIncludeParameter = "?propertiesToInclude={propertiesToInclude}";
        public const string ListOnlyOnlineStampsParameter = "?listOnlyOnline={listOnlyOnline}";
        public const string AssignToServerFarm = "?computeMode={computeMode}&serverFarm={serverFarmName}";
        public const string OperationStatusParameter = "?status={status}";
        public const string OperationStatusAndNameParameter = "?status={status}&operationName={operationName}";
        public const string SiteRepositoryActionParameter = @"action={action}";
        public const string UsersBatchParameter = "/usersBatch";
        public const string StateParameter = "/state";
        public const string OrgDomainParameter = "?orgDomain={orgDomain}";
        public const string GlobalPlanIdParameter = "&globalPlanId={globalPlanId}";
        public const string OwnerUserNameParameter = "ownerUserName={ownerUserName}";
        public const string CopyCertificatesTemplateParameter = "?targetWebSpaceName={targetWebSpaceName}&locationName={locationName}&stampName={stampName}&skipValidation={skipValidation}";
        public const string MigrateTemplateParameter = "?renameWebspaces={renameWebspaces}";
        public const string ForceArmSyncForSubscription = "/forceArmSyncForSubscription";
        public const string Rebalance = "/rebalance?newSmallTotal={newSmallTotal}&newMediumTotal={newMediumTotal}&newLargeTotal={newLargeTotal}&newFrontendTotal={newFrontendTotal}&newSharedWebWorkerTotal={newSharedWebWorkerTotal}&newSmallDSeriesTotal={newD1Total}&newMediumDSeriesTotal={newD2Total}&newLargeDSeriesTotal={newD3Total}&newX1Total={newX1Total}&newX2Total={newX2Total}&newX3Total={newX3Total}&overwrite={overwrite}";
        public const string WorkerSizeIdParameter = "&workerSizeId={workerSizeId}";
        public const string RepositoryUriProperty = "RepositoryUri";
        public const string PublishingUsernameProperty = "PublishingUsername";
        public const string PublishingPasswordProperty = "PublishingPassword";
        public const string MetadataProperty = "Metadata";
        public const string LinuxFxVersionProperty = "LinuxFxVersion";
        public const string WindowsFxVersionProperty = "WindowsFxVersion";
        public const string AppSettingsProperty = "AppSettings";
        public const string ScmTypeProperty = "ScmType";
        public const string SiteConfigProperty = "SiteConfig";
        public const string TrafficManagerProperty = "TrafficManager";
        public const string TrafficManagerAllProperty = "TrafficManagerAll";
        public const string FrontEndStatsParameter = "/clearfrontendstats";
        public const string SetFrontEndCountForQuickScaleUp = "/setFrontEndCountForQuickScaleUp?frontEndCount={frontEndCount}";
        public const string ForceCurrentDecision = "/forceCurrentDecision";
        public const string FastReimageWorker = "/fastreimage";
        public const string HostnameSslStateParameter = "/hostnamesslstate";

        // Service resources
        public const string ActiveSubscriptions = "subscriptions?activeFromDate={activeFromDate}&activeToDate={activeToDate}";
        public const string Backup = "/backup";
        public const string BackupConfig = "/backup/config";
        public const string BackupStatusIdParameter = "/backups/{backupId}";
        public const string ChangeSitesDefaultDnsSuffix = "changeSitesDefaultDnsSuffix";
        public const string ContainerCapacities = "containerCapacities";
        public const string StampCapacityTargets = "stampCapacityTargets";
        public const string StampWorkerBuffers = "stampWorkerBuffers";
        public const string Restore = "/restore";
        public const string RestoreDiscover = "/restore/discover";
        public const string Export = "/export";
        public const string Import = "/import?skipDnsRegistration={skipDnsRegistration}";
        public const string StorageMigrate = "/storageMigrate";
        public const string MigrateMySql = "/migrateMySql";
        public const string MigrateMySqlStatus = "/migrateMySqlStatus";
        public const string MySqlDumpParams = "/mySqlDumpParams";
        public const string Snapshots = "/snapshots";
        public const string SnapshotTimes = "/snapshots?getAllSnapshots={getAllSnapshots}&useDRSecondary={useDRSecondary}";
        public const string DeletedSiteSnapshots = "/deletedSiteSnapshots?deletedSiteCorrelationId={deletedSiteCorrelationId}";
        public const string RestorableSnapshots = "/restorableSnapshots";
        public const string InternalSnapshotData = "/internalSnapshotData?snapshotTime={snapshotTime}&force={force}&deletedSiteCorrelationId={deletedSiteCorrelationId}&useDRSecondary={useDRSecondary}";
        public const string StorageContainerSasUri = "/storageContainerSasUri";
        public const string Root = "";
        public const string RolesRoot = "roles";
        public const string EntitiesRoot = "entities";
        public const string UserRoles = "/userRoles";
        public const string HybridConnectionRelaysTopLevel = "/hybridConnectionRelays";
        public const string HybridConnectionNamespaces = "/hybridConnectionNamespaces";
        public const string HybridConnectionPlanLimits = "/hybridConnectionPlanLimits/limit";
        public const string HybridConnectionSites = "/sites";
        public const string HybridConnectionNamespaceNameTemplate = "/{namespaceName}";
        public const string HybridConnectionRelays = "/relays";
        public const string HybridConnectionRelayNameTemplate = "/{relayName}";
        public const string HybridConnection = "/hybridconnection";
        public const string PrivateAccess = "/privateAccess/virtualNetworks";
        public const string PrivateAccessVnets = "/privateAccessVnets";
        public const string PrivateAccessVnetsWithNameTemplate = PrivateAccessVnets + "/{vnetName}";
        public const string PrivateAccessSubnets = PrivateAccessVnetsWithNameTemplate + "/subnets";
        public const string PrivateAccessSubnetsWithNameTemplate = PrivateAccessSubnets + "/{subnetName}";
        public const string HybridConnectionExists = "/hybridconnectionexists";
        public const string NetworkFeatures = "/networkFeatures?view={view}";
        public const string NetworkTraceStart = "/networkTrace/start";
        public const string StartNetworkTrace = "/startNetworkTrace";
        public const string NetworkTraceStartWithParameters = NetworkTraceStart + "?durationInSeconds={durationInSeconds}&maxFrameLength={maxFrameLength}&sasUrl={sasUrl}";
        public const string NetworkTraceStartOperation = "/networkTrace/startOperation";
        public const string NetworkTraceStartOperationWithParameters = NetworkTraceStartOperation + "?durationInSeconds={durationInSeconds}&maxFrameLength={maxFrameLength}&sasUrl={sasUrl}";
        public const string NetworkTraceStop = "/networkTrace/stop";
        public const string StopNetworkTrace = "/stopNetworkTrace";
        public const string ResetStorageQuota = "/resetfilestoragequota?forceReset={forceReset}";
        public const string OverwriteStorageQuota = "/overwritefilestoragequota?storageLimitGb={storageLimitGb}";
        public const string CloudEntityRoot = EntitiesRoot + UserRoles;
        public const string SubscriptionEntitiesRoot = EntitiesRoot + "/subscriptions/{subscriptionName}" + UserRoles;
        public const string WebSpaceEntitiesRoot = EntitiesRoot + "/subscriptions/{subscriptionName}/webspaces/{webspaceName}" + UserRoles;
        public const string WebSiteEntitiesRoot = EntitiesRoot + "/subscriptions/{subscriptionName}/webspaces/{webspaceName}/sites/{siteName}" + UserRoles;
        public const string GeoRegionsRoot = "regions";
        public const string GlobalQuotaResource = "globalquotas";
        public const string GlobalPlanResource = "globalplans";
        public const string GeoRegionsLocationsRoot = "regionslocations";
        public const string RegionalGeomastersRoot = "regionalgeomasters";
        public const string GeoRegion = "regions/{regionName}";
        public const string RegionNameQueryParameter = "?regionName={regionName}";
        public const string GeoPropertiesRoot = "properties";
        public const string GeoStorageConnectionString = "storageConnectionString";
        public const string GeoConfigurationRoot = "config";
        public const string RemoveRegionalToGlobalSyncState = "removeRegionalToGlobalSyncState";
        public const string BulkHostingConfigurationRoot = "bulkHostingConfiguration";
        public const string AseJobsUri = "asejobs?hostingEnvironmentName={hostingEnvironmentName}&jobName={jobName}&runAfter={runAfter}&additionalProperties={additionalProperties}";
        public const string FeaturesOrgDomainQueryParam = "orgDomain={orgDomain}";
        public const string FeaturesBaseQuery = "features?featureName={featureName}";
        public const string GeoLocationsRoot = "regions/{regionName}/locations";
        public const string CertRoleTemplateParameter = "/{role}";
        public const string ExtraAllowedCertRoot = "extraallowedcertthumbprints";
        public const string GlobalQuotaValue = "?globalPlanName={globalPlanName}&globalQuotaName={globalQuotaName}&maxValue={maxValue}&sku={sku}";
        public const string RapidUpdateFeedInfos = "rapidUpdateFeedInfos";
        public const string RolePatcherInfos = "rolePatcherFeedInfos";
        public const string Stamps = "stamps";
        public const string StampsRoot = "locations/{locationName}/stamps";
        public const string StampsInRegionRoot = "regions/{regionName}/stamps";
        public const string HostingEnvironmentsRoot = "subscriptions/{subscriptionName}" + Csm.HostingEnvironments;
        public const string skipEnvironmentValidationsAndCreateState = "?skipEnvironmentValidations={skipEnvironmentValidations}&createState={createState}";
        public const string skipEnvironmentValidations = "?skipEnvironmentValidations={skipEnvironmentValidations}";
        public const string StampsCommand = "?Command={command}&CommandArgs={commandArgs}";
        public const string GeoRegionServicesRoot = "georegionservices";
        public const string RegionalGeomasterServicesRoot = "regionalgeomasters";
        public const string WebSitesRoot = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/sites";
        public const string WebSpacesRoot = "{subscriptionName}/webspaces";
        public const string WebSpacesVnetInfo = "{subscriptionName}/webspaces/{webspaceName}/VNETInfos";
        public const string WebSpacesSwiftEnabled = "{subscriptionName}/webspaces/{webspaceName}/SwiftEnabled";
        public const string CertificatesRoot = "{subscriptionName}/webspaces/{webspaceName}/certificates";
        public const string CertificatesTemplateParameter = "?skipValidation={skipValidation}";
        public const string CsrsRoot = "{subscriptionName}/webspaces/{webspaceName}/csrs";
        public const string StacksRoot = "{subscriptionName}/webspaces/{webspaceName}/stacks";
        public const string ServerFarmsRoot = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/serverfarms";
        public const string ServerFarmPerformance = ServerFarmsRoot + NameTemplateParameter + "/performance";
        public const string SyncServerfarm = ServerFarmsRoot + NameTemplateParameter + "/sync";
        public const string ServerFarmMetricDefinitions = ServerFarmsRoot + NameTemplateParameter + "/metricdefinitions";
        public const string ServerFarmMetricsRoot = ServerFarmsRoot + NameTemplateParameter + "/metrics";
        public const string ServerFarmUsages = ServerFarmsRoot + NameTemplateParameter + "/usages?names={usageNames}";
        public const string ServerFarmCanary = ServerFarmsRoot + NameTemplateParameter + "/canary";
        public const string ServerFarmVirtualNetworkConnections = ServerFarmsRoot + NameTemplateParameter + "/virtualNetworkConnections";
        public const string ServerFarmSwiftConnectionCount = ServerFarmsRoot + NameTemplateParameter + "/swiftNetworks?query=count";
        public const string ServerFarmSwiftEnabled = ServerFarmsRoot + NameTemplateParameter + "/SwiftEnabled";
        public const string ServerFarmRebootWorker = ServerFarmsRoot + NameTemplateParameter + "/workers/{workerName}/reboot";
        public const string ServerFarmBeginWorkerNetworkTrace = ServerFarmsRoot + NameTemplateParameter + "/workers/{workerName}?action=beginNetworkTrace";
        public const string ServerFarmEndWorkerNetworkTrace = ServerFarmsRoot + NameTemplateParameter + "/workers/{workerName}?action=endNetworkTrace";
        public const string ServerFarmRestartSites = ServerFarmsRoot + NameTemplateParameter + "/restartSites?softRestart={softRestart}";
        public const string ServerFarmCanaryRestart = ServerFarmsRoot + NameTemplateParameter + "/canary/restart";
        public const string ServerFarmCanaryStop = ServerFarmsRoot + NameTemplateParameter + "/canary/stop";
        public const string ServerFarmSites = ServerFarmsRoot + NameTemplateParameter + Sites + "?marker={marker}&pageSize={pageSize}&runningSitesOnly={runningSitesOnly}&includeSlots={includeSlots}";
        public const string ServerFarmFirstPartyAppSettings = ServerFarmsRoot + NameTemplateParameter + "/firstPartyApps/{firstPartyAppId}/settings";
        public const string ServerFarmSpecificFirstPartyAppSetting = ServerFarmsRoot + NameTemplateParameter + "/firstPartyApps/{firstPartyAppId}/settings/{settingName}";
        public const string ServerFarmMoveBaseUri = ServerFarmsRoot + NameTemplateParameter + "/move?destSubscriptionName={destSubscriptionName}&destWebspaceName={destWebspaceName}";
        public const string ServerFarmMoveWebSystemQueryString = "&destWebSystem={destWebSystem}";
        public const string ServerFarmChangeWebSystem = ServerFarmsRoot + NameTemplateParameter + "/changeWebSystem?destWebsytem={destWebSystem}";
        public const string ServerFarmHybridConnectionRelaysTopLevel = ServerFarmsRoot + NameTemplateParameter + HybridConnectionRelaysTopLevel;
        public const string ServerFarmHybridConnectionRelaysFullPath = ServerFarmsRoot + NameTemplateParameter + HybridConnectionNamespaces +
                HybridConnectionNamespaceNameTemplate + HybridConnectionRelays + HybridConnectionRelayNameTemplate;
        public const string ServerFarmHybridConnectionPlanLimit = ServerFarmsRoot + NameTemplateParameter + HybridConnectionPlanLimits;
        public const string ServerFarmHybridConnectionSites = ServerFarmHybridConnectionRelaysFullPath + HybridConnectionSites;
        public const string ServerFarmCapabilities = ServerFarmsRoot + NameTemplateParameter + "/capabilities";
        public const string ServerFarmEventGrid = ServerFarmsRoot + NameTemplateParameter + "/eventGrid";
        public const string WebSystemsRoot = "websystems";
        public const string WebSpaceUsagesRoot = "{subscriptionName}/webspaces/{webspaceName}/usages?names={usages}&computeMode={computeMode}&siteMode={siteMode}";
        public const string WebSiteNamedRoot = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/sites/{name}";
        public const string WebSiteUsagesRoot = WebSiteNamedRoot + "/usages?names={usages}&computeMode={computeMode}&siteMode={siteMode}";
        public const string WebSiteMetricsRoot = WebSiteNamedRoot + "/metrics";
        public const string WebSitePerfMonRoot = WebSiteNamedRoot + "/perfcounters";
        public const string WebSiteMetricDefinitions = WebSiteNamedRoot + "/metricdefinitions";
        public const string WebSiteConfig = WebSiteNamedRoot + "/config";
        public const string WebSiteConfigSnapshots = WebSiteNamedRoot + "/config/snapshots";
        public const string WebSiteConfigSnapshot = WebSiteNamedRoot + "/config/snapshots/{snapshotId}";
        public const string WebSiteConfigReferenceAppSettings = WebSiteNamedRoot + "/config/configreferences/appsettings";
        public const string WebSiteConfigReferenceAppSetting = WebSiteNamedRoot + "/config/configreferences/appsettings/{appSettingKey}";
        public const string WebSitePhpLoggingSettings = WebSiteNamedRoot + "/phplogging";
        public const string WebSiteEventGrid = WebSiteNamedRoot + "/eventGrid";
        public const string WebSiteAppSettings = WebSiteNamedRoot + "/appsettings";
        public const string WebSiteDRRootPath = WebSiteNamedRoot + "/drrootpath";
        public const string WebSiteConnectionStrings = WebSiteNamedRoot + "/connectionstrings";
        public const string WebSiteAzureStorageAccounts = WebSiteNamedRoot + "/azurestorageaccounts";
        public const string WebSiteAuthSettings = WebSiteNamedRoot + "/authsettings";
        public const string WebSiteAuthSettingsV2 = WebSiteNamedRoot + "/authsettingsV2";
        public const string WebSitePublishingCredentials = WebSiteNamedRoot + "/publishingcredentials";
        public const string WebSitePushSettings = WebSiteNamedRoot + "/pushsettings";
        public const string WebSiteMetadata = WebSiteNamedRoot + "/metadata";
        public const string WebSiteTriggers = WebSiteNamedRoot + "/triggers";
        public const string WebSiteLogsConfig = WebSiteNamedRoot + "/config/logs";
        public const string WebSiteSandboxConfig = WebSiteNamedRoot + "/sandbox";
        public const string WebSiteSourceControl = WebSiteNamedRoot + "/sourcecontrols";
        public const string WebSiteHostRuntime = WebSiteNamedRoot + "/hostruntime/{*path}";
        public const string WebSiteExtensions = WebSiteNamedRoot + "/extensions/{*path}";
        public const string WebSitePremierAddOn = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/sites/{siteName}/premieraddons";
        public const string WebSitePremierAddOnOffers = "subscriptions/{subscriptionName}/premieraddonoffers";
        public const string WebSiteRepository = WebSiteNamedRoot + "/repository";
        public const string WebSiteRepositorySync = WebSiteNamedRoot + "/repository?action=sync";
        public const string WebSiteAuditLogs = WebSiteNamedRoot + "/auditlogs?startTime={startTime}&endTime={endTime}";
        public const string WebSiteGetLastAuditLog = WebSiteNamedRoot + "/lastauditlog";
        public const string WebSiteSwap = WebSiteNamedRoot + "?Command={command}&OtherSiteName={otherSiteName}";
        public const string WebSiteSlotSwap = WebSiteNamedRoot + "/slots?Command={command}&targetSlot={targetSlotName}";
        public const string StatefulWebSiteSlotSwap = WebSiteNamedRoot + "/statefulswap";
        public const string WebSiteSlotClone = WebSiteNamedRoot + "/slotCopy?targetSlot={targetSlotName}";
        public const string ApplySlotConfig = WebSiteNamedRoot + "/applySlotConfig?targetSwapSlot={targetSwapSlot}";
        public const string ResetSlotConfig = WebSiteNamedRoot + "/resetSlotConfig";
        public const string SlotConfigNames = WebSiteNamedRoot + "/slotConfigNames";
        public const string BasicPublishingCredentialsPoliciesPath = WebSiteNamedRoot + "/basicpublishingcredentialspolicies/{publishingResource}";
        public const string WebSiteSlotCompare = WebSiteNamedRoot + "/slotsdiffs?targetSlot={targetSlotName}";
        public const string WebSiteRestart = WebSiteNamedRoot + "/restart?softRestart={softRestart}&synchronous={synchronous}";
        public const string WebSiteStart = WebSiteNamedRoot + "/start";
        public const string WebSiteStop = WebSiteNamedRoot + "/stop";
        public const string WebSiteChangeHostNameState = WebSiteNamedRoot + "/changeHostNameState?action={action}&useSecondaryInputStamp={useSecondaryInputStamp}";
        public const string WebSiteIsValidCustomDomain = WebSiteNamedRoot + "/isvalidcustomdomain?hostName={hostName}&type={recordType}&skipCustomDomainVerification={skipCustomDomainVerification}";
        public const string WebSiteAnalyzeCustomHostname = WebSiteNamedRoot + "/analyzeCustomHostname?hostName={hostName}";
        public const string WebSiteGetInstanceIdentifiers = WebSiteNamedRoot + "/instanceids";
        public const string WebSiteAdminInitializationScripts = WebSiteNamedRoot + "/adminInitializationScripts";
        public const string WebSiteRecover = WebSiteNamedRoot + "/recover?snapshotTime={snapshotUtcTime}&recoverConfig={recoverConfig}&targetSiteNameWithSlot={targetSiteNameWithSlot}&forceRecovery={forceRecovery}";
        public const string WebSiteRecoverConfig = WebSiteNamedRoot + "/recoverConfig?snapshotId={snapshotId}&forceRecovery={forceRecovery}";
        public const string WebSitePrivateLinkIdentifiers = WebSiteNamedRoot + "/privateLinkIdentifiers";
        public const string WebSiteSwiftNetwork = WebSiteNamedRoot + "/swiftNetwork";
        public const string WebSiteNamedSwiftNetwork = WebSiteSwiftNetwork + "/{vnetName}";
        public const string WebSiteNamedSwiftNetworkToken = WebSiteNamedSwiftNetwork + "/token/{token}";
        public const string SafeRebalance = "safeRebalance";
        public const string AutomorphismUpdateConfig = "automorphismUpdateConfig?stampNames={stampNames}";
        public const string SetAutoStateControlStampSwitch = "setAutoStateControlStampSwitch?stampNames={stampNames}&value={value}&dSeriesSwitch={dSeriesSwitch}&xSeriesSwitch={xSeriesSwitch}";
        public const string SetAutoDynamicStateControlStampSwitch = "setAutoDynamicStateControlStampSwitch?stampNames={stampNames}&value={value}";
        public const string SetAutoStateControlReserveCapacityForStamps = "setAutoStateControlReserve?stampNames={stampNames}&value={value}&dSeriesValue={dSeriesValue}&xSeriesValue={xSeriesValue}";
        public const string SetAutoDynamicStateControlReserveCapacityForStamps = "setAutoDynamicStateControlReserveCapacityForStamps?stampNames={stampNames}&value={value}";
        public const string AddResourceNavigationLinkToScavenger = "addResourceNavigationLinkToScavenger?hostingEnvironmentName={hostingEnvironmentName}&vnetSubId={vnetSubId}&vnetSubnetName={vnetSubnetName}&vnetId={vnetId}&operationId={operationId}";
        public const string SetAutoStateControlSwitch = "setAutoStampStateSyncGlobalSwitch?value={value}";
        public const string GetAutoStateControlStatus = "getAutoStateControlStatus";
        public const string SafeFastRebalance = "safeFastRebalance";
        public const string SafeRebalanceWithAllTenants = "safeRebalanceWithAllTenants";
        public const string WalkUpgradeDomainForManualUpgrade = "walkUpgradeDomainForManualUpgrade?upgradeDomain={upgradeDomain}&serviceName={serviceName}&force={force}";
        public const string PauseRunningManualUpgrade = "pauseRunningManualUpgrade";
        public const string ResumePausedManualUpgrade = "resumePausedManualUpgrade";
        public const string SetDatabaseSku = "setDatabaseSku";
        public const string Sites = "/sites";
        public const string GlobalUsages = "/usages?names={usageNames}";
        public const string DatabaseReadOnlyView = "DatabaseReadOnlyView?serviceName={serviceName}&databaseName={databaseName}&userName={userName}";
        public const string GeomasterReadOnlyView = "GeomasterReadOnlyView";
        public const string EntityEventNotification = "entityEventNotification";
        public const string MigrateSite = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/sites/{siteName}/migrationJobs";
        public const string NotifySiteMigrationStatus = WebSiteNamedRoot + "/migrationJobs/notify?migrationStatus={migrationStatus}";

        public const string SyncDbState = "/syncDbState";

        public const string GetWebSiteNames = "getWebSiteNames/";
        public const string GetWebSpaceNames = "getWebSpaceNames/";
        public const string RecordCount = "recordCount/{recordCount}/";
        public const string StartAfterName = "startAfterName/{*startAfterName}";
        public const string GetSiteDetailsPaged = Web + "/" + GetWebSiteNames + RecordCount + StartAfterName;
        public const string GetWebSpaceDetailsPaged = Web + "/" + GetWebSpaceNames + RecordCount + StartAfterName;
        public const string CheckWebSpaceExists = Web + "/" + WebSpacesRoot + NameTemplateParameter + ExistsParameter;
        public const string GetUnsynchronizedElements = "getUnsynchronizedElements";

        public const string AvailableStacksResource = "availableStacks";
        public const string RuntimeStacksResource = "runtimeStacks";
        public const string GetAvailableStacks = "?osTypeSelected={osTypeSelected}";

        public const string WebSpaceOperationsRoot = "{subscriptionName}/webspaces/{webspaceName}/operations";
        public const string WebSiteOperationsRoot = WebSiteNamedRoot + "/operations";
        public const string ServerFarmOperationsRoot = "subscriptions/{subscriptionName}/webspaces/{webspaceName}/serverfarms/{name}/operations";
        public const string WebSiteNetworkTracesRoot = WebSiteNamedRoot + "/networkTrace";
        public const string WebSiteNetworkTraceOperationsRoot = WebSiteNamedRoot + "/networkTrace/operations";
        public const string OperationIdParameter = "/{operationId}";
        public const string NameParameter = "/{name}";
        public const string WebSpaceNamedRoot = "{subscriptionName}/webspaces/{webspaceName}/migrate";
        public const string MigrateWebspaceTemplateParameter = "?destWebSpaceName={destWebSpaceName}";
        public const string ChangeWebspaceWebsystemRoot = "{subscriptionName}/webspaces/{webspaceName}/changeWebSystem";
        public const string ChangeWebspaceTemplateParameter = "?destWebSpaceName={destWebSpaceName}&destWebSystem={destWebSystem}";

        public const string HostNameAvailability = "ishostnameavailable/{subDomain}?isFQDN={isFQDN}";
        public const string PublishingUserNameAvailability = "isusernameavailable/{name}";

        public const string HostNameReservedOrNotAllowed = "ishostnamereservedornotallowed/{subDomain}?isFQDN={isFQDN}";

        public const string CanarySites = "canarysites";

        public const string WebSitesPerSubscription = "subscriptions/{subscriptionName}/sites";
        public const string DeletedWebSitesPerSubscription = "subscriptions/{subscriptionName}/deletedsites";

        public const string SubscriptionPublishingUsers = "{subscriptionName}/publishingUsers";
        public const string SubscriptionPublishingCredentials = "{subscriptionName}/publishingCredentials";
        public const string SubscriptionGeoRegions = "{subscriptionName}/geoRegions";
        public const string SubscriptionDeletedSites = "{subscriptionName}/deletedSites";
        public const string SubscriptionHostNames = "{subscriptionName}/hostNames?integrationType={integrationType}";
        public const string SubscriptionVerifyTrafficManagerConfiguration = "{subscriptionName}/verifyTrafficManagerConfiguration?trafficManagerDomainName={trafficManagerDomainName}&registerTrafficManagerDomainName={registerTrafficManagerDomainName}&failIneligibleSites={failIneligibleSites}";
        public const string SubscriptionConfirmTrafficManagerConfiguration = "{subscriptionName}/confirmTrafficManagerConfiguration?trafficManagerDomainName={trafficManagerDomainName}";
        public const string SubscriptionConfirmTrafficManagerConfigurationAndFetch = "{subscriptionName}/confirmTrafficManagerConfigurationAndFetch?trafficManagerDomainName={trafficManagerDomainName}";
        public const string SubscriptionUnregisterTrafficManagerConfiguration = "{subscriptionName}/unregisterTrafficManagerConfiguration?trafficManagerDomainName={trafficManagerDomainName}";
        public const string DnsSuffix = "{subscriptionName}/dnsSuffix";
        public const string SubscriptionOrgDomains = "subscriptions/{subscriptionName}?orgDomains={orgDomains}";
        public const string WebSitePublishingProfile = WebSiteNamedRoot + "/publishxml?format={format}&useDRSecondary={useDRSecondary}";
        public const string WebSiteGenerateNewPassword = WebSiteNamedRoot + "/newpassword";
        public const string WebSiteContainerLogsZip = WebSiteNamedRoot + "/containerlogs/zip";
        public const string WebSiteStatus = WebSiteNamedRoot + "/status";
        public const string WebSiteContainerLogs = WebSiteNamedRoot + "/containerlogs";

        //webapp anti-virus scan operations
        public const string StartWebsiteAntiVirusScan = WebSiteNamedRoot + "/scan/start?timeout={timeout}";
        public const string TrackWebsiteAntiVirusScan = WebSiteNamedRoot + "/scan/track/{trackId}";
        public const string WebsiteAntiVirusScanResult = WebSiteNamedRoot + "/scan/result/{scanId}";
        public const string ListWebsiteAntiVirusScanResult = WebSiteNamedRoot + "/scan/results";
        public const string StopWebsiteAntiVirusScan = WebSiteNamedRoot + "/scan/stop";

        public const string RDFENotification = "notification";
        public const string StaleResourcesParameter = "/staleresources?region={regionName}&whatIf={whatIf}";

        //operational configuration store
        public const string ConfigStoreRoot = "configstore";
        public const string Create = "/Create";
        public const string Preview = "/Preview";
        public const string Update = "/Update";
        public const string CurrentValues = "/CurrentValue/{path}/{settingName}?buildVersion={buildVersion}";
        public const string Publish = "/Publish";
        public const string Delete = "/Delete";
        public const string UpdateAndPublish = "/UpdateAndPublish";
        public const string HistoryValue = "/HistoryValue/{path}/{settingName}";

        public const string Systems = "systems";
        public const string Servers = "systems/{webSystemName}/servers";
        public const string WebWorkers = "systems/{webSystemName}/webworkers";
        public const string LoadBalancers = "systems/{webSystemName}/loadbalancers";
        public const string Publishers = "systems/{webSystemName}/publishers";
        public const string Controllers = "systems/{webSystemName}/controllers";
        public const string FileServers = "systems/{webSystemName}/fileservers";
        public const string FileServerUnmount = "systems/{webSystemName}/fileservers/{name}/unmount";
        public const string ManagementServers = "systems/{webSystemName}/managementservers";
        public const string WebPlan = "/{name}/web";
        public const string WebQuotas = "/{planName}/web/quotas";
        public const string Policies = "/{planName}/web/policies";
        public const string SystemSites = "systems/{name}/sites?filter={filter}&pageNumber={pageNumber}&pageSize={pageSize}&details={details}&orderBy={orderBy}";
        public const string Credentials = "systems/{webSystemName}/credentials";
        public const string RuntimeState = "systems/{webSystemName}/runtimestates/{name}";

        public const string SystemMetrics = "systems/{name}/metrics";
        public const string WebWorkerMetrics = "systems/{webSystemName}/webworkers/{name}/metrics";
        public const string LoadBalancerMetrics = "systems/{webSystemName}/loadbalancers/{name}/metrics";
        public const string PublisherMetrics = "systems/{webSystemName}/publishers/{name}/metrics";
        public const string ControllerMetrics = "systems/{webSystemName}/controllers/{name}/metrics";
        public const string FileServerMetrics = "systems/{webSystemName}/fileservers/{name}/metrics";
        public const string ManagementServerMetrics = "systems/{webSystemName}/managementservers/{name}/metrics";

        public const string InstanceMetrics = "/instanceMetrics";
        public const string InstanceMetricsDefinitions = "/instanceMetricDefinitions";
        public const string HttpThrottlerSettings = "/httpThrottlerSettings";
        public const string SslThrottlerSettings = "/sslThrottlerSettings";
        public const string BlockedHostNames = "/blockedHosts";

        public const string WorkerSites = "systems/{webSystemName}/webworkers/{name}/sites?filter={filter}&pageNumber={pageNumber}&pageSize={pageSize}&details={details}&orderBy={orderBy}";
        public const string SpareWebWorkers = "systems/{webSystemName}/webworkers/spare";
        public const string SpareWebWorkersBySize = "systems/{webSystemName}/webworkers/spareBySize";
        public const string WebWorkerReadyForLoadBalancing = "systems/{webSystemName}/webworkers/{name}/readyforloadbalancing";

        public const string WorkerSize = "webworkers/size";
        public const string FeedEntry = "webworkers/feedEntry?productId={productId}";
        public const string ProductIdParameter = "/{productId}";
        public const string WorkerSizeParameter = "?workerSize={workerSize}&computeMode={computeMode}&isLinux={isLinux}&isXenon={isXenon}";
        public const string ActiveOnlyParameter = "?activeOnly={activeOnly}";

        public const string WorkerTier = "workerTiers/{name}";

        public const string SystemSettings = "systems/{name}/config";
        public const string SystemLog = "systems/{name}/log?startId={startId}&recordCount={recordCount}";
        public const string SystemLogWithInfo = "systems/{name}/logwithinfo?startId={startId}&recordCount={recordCount}";
        public const string SystemLogWithDateRange = "systems/{name}/logwithdate?startTime={startTime}&cutOffTime={cutOffTime}";

        public const string SystemSummary = "systems/{name}/summary";

        public const string HostingConfigurations = "configurations/hostingconfigurations/{configurationKey}";

        public const string DataRuntimeSite = "dataruntime/sites/{siteName}";
        public const string DataRuntimeSiteWorkers = "dataruntime/sites/{siteName}/runningworkers";
        public const string DataRuntimeSiteProfile = "dataruntime/sites/{siteName}/profile";
        public const string DataRuntimeWebWorkers = "dataruntime/webworkers";
        public const string DataRuntimeWebWorkerRunningSites = DataRuntimeWebWorkers + NameTemplateParameter + "/runningsites";
        public const string DataRuntimeRunningSites = "dataruntime/runningsites";
        public const string DataRuntimeEntities = "dataruntime/{entityType}";
        public const string DataRuntimeVirtualFarm = "dataruntime/subscriptions/{subscriptionName}/webspaces/{webspaceName}/vritualfarms/{virtualfarmName}";
        public const string DataRuntimeCountEntities = "dataruntime/count/{entityType}";
        public const string DataRuntimeEntity = "dataruntime/{entityType}/{entityName}";
        public const string DataRuntimeResetCacheLease = "dataruntime/resetcache";
        public const string DataRuntimeResetCacheNonLease = "dataruntime/resetcachenonlease";
        public const string DataRuntimeForceCacheRefresh = "dataruntime/refreshcache";
        public const string RefreshVersions = "dataruntime/refreshversions";
        public const string DataServiceMetadata = "dataruntime/metadata";
        public const string DataRuntimeForceCacheRefreshWritableEntities = "dataruntime/refreshcachewritableentities";
        public const string DataRuntimePlaceholderPool = "dataruntime/placeholderpool";

        public const string GetServerFarmsWithoutWorkers = "serverfarmrecovery/serverFarmsWithoutWorkers?expirationPeriodInMinutes={expirationPeriodInMinutes}&batchSize={batchSize}";

        public const string FirstPartyAppSettings = "firstPartyAppSettings";

        public const string ConfigStoreSas = "ConfigStore/sasKey?duration={sasDurationInHours}&createIfNotExist={isCreateStorageContainerIfNotExist}&isWritable={isWritable}";
        public const string StorageBlobSas = "storageBlob/{containerName}/sasKey?duration={sasDurationInHours}";
        public const string DatabaseRoConnection = "dbRoConn/{databaseName}/connStr?userName={userName}";
        public const string ReencryptDB = "database/reencryptDatabase";
        public const string GetDBReencryptOperation = "database/getReencryptionOperation?operationId={operationId}";
        public const string ConfigStoreSasFormat = "ConfigStore/sasKey?duration={0}&createIfNotExist={1}&isWritable={2}";
        public const string StorageBlobSasFormat = "storageBlob/{0}/sasKey?duration={1}";
        public const string StorageMeteringSasFormat = "storageMetering/sasKey?duration={sasDurationInHours}";
        public const string DatabaseRoConnectionFormat = "dbRoConn/{0}/connStr?userName={1}";

        public const string DisasterRecoveryStampInformation = "disasterrecovery/stampinformation";
        public const string DisasterRecoveryGenerateKeys = "disasterrecovery/key?generateNewKey={generateNewKey}";
        public const string EncryptDisasterRecoveryCredentials = "disasterrecovery/encrypt";
        public const string DisasterRecoveryPickCredentials = "disasterrecovery/credentials";
        public const string DisasterRecoveryFailoverStamp = "startdisasterrecoveryfailover?stampIdentifier={stampIdentifier}";

        public const string PerformMachineOperation = "machines/{role}/{name}/{operationType}?serviceName={serviceName}";
        public const string PerformMachineOperationGeoLevel = "machines/{role}/{name}/{operationType}?locationName={locationName}&stampName={stampName}";

        public const string AddDedicatedVmssWorkers = "VmssDedicatedWorkers/Add/";
        public const string RemoveDedicatedVmssWorkers = "VmssDedicatedWorkers/Remove/";
        public const string WorkerSizeCountAndOSFlavor = "?workerName={workerName}&Count={Count}";

        public const string AddRoleInstance = "Vmss/Add/";
        public const string RemoveRoleInstance = "Vmss/Remove/";
        public const string VmssSkuGetState = "Vmss/Sku/State/";
        public const string VmssSkuUpdate = "Vmss/Sku/Update";
        public const string GetSkuState = "?Role={role}";
        public const string SkuChangeQuery = "?Role={role}&newSku={newSku}";
        public const string RoleInstanceChangeQuery = "?Role={role}&Count={count}";

        public const string ShrinkVipMappingsAndReturnAll = "shrinkvipmappingsandreturnall?desiredCount={desiredCount}&serviceName={serviceName}";

        public const string PerformEnvironmentCheck = "performenvironmentcheck";

        public const string VipMappings = "vipmappings";
        public const string VirtualIP = "/{virtualIP}";
        public const string IpAddresses = "ipAddresses";
        public const string OutboundNetworkDependenciesEndpoints = "/outboundNetworkDependenciesEndpoints";
        public const string InboundNetworkDependenciesEndpoints = "/inboundNetworkDependenciesEndpoints";
        public const string GetSwiftDetailsForHostingEnvironment = "getSwiftDetailsForHostingEnvironment?hostingEnvironmentName={hostingEnvironmentName}";
        public const string VerifySubscriptionRegionalAccess = "verifySubscriptionRegionalAccess?subscriptionName={subscriptionName}";
        public const string SyncHostingEnvironmentScalingState = "syncHostingEnvironmentScalingState?hostingEnvironmentName={hostingEnvironmentName}";

        public const string SitesUsingSwift = "sitesUsingSwift/{subscriptionName}/{resourceGroup}/{vnetName}/{subnetName}";
        public const string InvokeDnc = "invokeDnc";
        public const string NrpServiceAssociationLinkShim = "nrpServiceAssociationLinkShim/{subscriptionName}/{resourceGroup}/{vnetName}/{subnetName}/{salName}";
        public const string NrpServiceAssociationDeleteIfOrphaned = "nrpServiceAssociationDeleteIfOrphaned/{subscriptionName}/{resourceGroup}/{vnetName}/{subnetName}";

        public const string PrivateEndpointConnectionProxies = "/privateEndpointConnectionProxies";
        public const string PrivateEndpointConnections = "/privateEndpointConnections";
        public const string PrivateLinkResources = "/privateLinkResources";
        public const string PrivateEndpointConnectionName = "/{privateEndpointConnectionName}";
        public const string PrivateEndpointConnectionProxyName = "/{privateEndpointConnectionProxyName}";
        public const string PrivateEndpointConnectionProxiesValidate = "/validate";
        public const string PrivateLinkHostingEnvironmentUpdateShim = "forceUpdateHostingEnvironmentPrivateLink?hostingEnvironmentName={hostingEnvironmentName}&internalFqdn={internalFqdn}";
        public const string PrivateLinkSiteUpdateShim = "forceUpdateSitePrivateLink?subscriptionId={subscriptionId}&resourceGroupName={resourceGroupName}&siteWithSlotName={siteWithSlotName}&privateEndpointConnectionName={privateEndpointConnectionName}&internalFqdn={internalFqdn}";

        public const string GetNetworkPolicies = "/GetNetworkPolicies";

        public const string ServiceDeployment = "serviceDeployment?stampName={stampName}";

        public const string MeteringSettings = "meteringsettings";
        public const string GetScaleDownCandidates = "getScaleDownCandidates?stampName={stampName}&targetSku={targetSku}&activeFromDate={activeFromDate}&activeToDate={activeToDate}&offerTypes={offerTypes}&whatIf={whatIf}";
        public const string MeteringParameters = "/type/{parameterType}/name/{parameterName}";
        public const string StampNamesParameter = "?stampNames={stampNames}";

        public const string BuiltInPowerShellDnsProviders = "BuiltInPowerShellDnsProviders";
        public const string CopyBuiltInPowerShellDnsScriptsToCustom = "CopyBuiltInPowerShellDnsScriptsToCustom";

        public const string WebWorkerRole = "webworkers";
        public const string LoadBalancerRole = "loadbalancers";
        public const string PublisherRole = "publishers";
        public const string ControllerRole = "controllers";
        public const string FileServerRole = "fileservers";
        public const string ManagementServerRole = "managementservers";

        public const string FetchEncryptedCredentials = "fetchencryptedcredentials";
        public const string DisasterRecoveryFailover = "disasterrecoveryfailover?failingLocationName={failingLocationName}&failingStampName={failingStampName}&helpingLocationName={helpingLocationName}&helpingStampName={helpingStampName}&makeDnsChanges={makeDnsChanges}";
        public const string RecordDeploymentExecution = "recorddeploymentexecution";
        public const string CleanAuditLogs = "auditlogs?timestamp={timestamp}";
        public const string CleanSiteChangeLogs = "changelogs?timestamp={timestamp}";
        public const string CleanApiOperations = "operations?timestamp={timestamp}";
        public const string DowngradeSites = "DowngradeSites";
        public const string RevertSitesDowngrade = "RevertSitesDowngrade";
        public const string SwitchSitesToStorageRecoveryRunningMode = "SwitchSitesToStorageRecoveryRunningMode";
        public const string PerformAzureEnvironmentInfoBackpropagation = "AzureEnvironmentInfoBackpropagation";
        public const string StartWebDeployMirroring = "StartWebDeployMirroring";
        public const string StopWebDeployMirroring = "StopWebDeployMirroring";
        public const string EnableWebDeployPublish = "EnableWebDeployPublish";
        public const string DisableWebDeployPublish = "DisableWebDeployPublish";
        public const string ResetWebDeployMirroringState = "ResetWebDeployMirroringState";
        public const string ContinuousCopyMonitoring = "ContinuousCopyMonitoring";
        public const string LocalContinuousCopyMonitoring = "LocalContinuousCopyMonitoring";
        public const string SnapshotDatabaseCopy = "SnapshotDatabaseCopy";
        public const string AllowSuperAdminPublishingCredential = "allowsuperadminpublishingcredential";
        public const string DisableSuperAdminPublishingCredential = "disablesuperadminpublishingcredential";
        public const string WorkerSanitizingSuspended = "WorkerSanitizingSuspended";
        public const string WorkerSanitizingAllDrives = "WorkerSanitizingAllDrives";
        public const string ApiTest = "ApiTest";
        public const string ReservedStorageVolumeCount = "ReservedStorageVolumes/count";
        public const string ProcessTracing = "ProcessTracing";
        public const string UseGeoRegionService = "UseGeoRegionService";
        public const string SetupMinistampSwift = "SetupMinistampSwift";
        public const string SetHumanInvestigate = "sethumaninvestigate?upgradeDomain={upgradeDomain}&humanInvestigateMode={humanInvestigateMode}";
        public const string AllOutboundIpAddresses = "AllOutboundIpAddresses";
        public const string UpdateAllowedInboundIpAddressesToDRFS = "UpdateAllowedInboundIpAddressesToDRFS";
        public const string UpdateInboundIpParametersGeo = "?locationName={locationName}&stampName={stampName}";
        public const string FinishAseV1Migration = "FinishAseMigration?stampName={stampName}";
        public const string ResumeHostingEnvironment = "ResumeHostingEnvironment?subscriptionName={subscriptionName}&hostingEnvironmentName={hostingEnvironmentName}&manualResumeOnly={manualResumeOnly}";
        public const string SuspendHostingEnvironment = "SuspendHostingEnvironment?subscriptionName={subscriptionName}&hostingEnvironmentName={hostingEnvironmentName}";
        public const string ForceHostingEnvironmentUnhealthy = "ForceHostingEnvironmentUnhealthy?hostingEnvironmentName={hostingEnvironmentName}&description={description}";
        public const string UpdateAseOperationFreezeStatus = "UpdateAseOperationFreezeStatus?operationId={operationId}&isFrozen={isFrozen}";
        public const string DeployMSHA = "DeployMSHA?subscriptionName={subscriptionName}&webspaceName={webspaceName}&name={name}";
        public const string SetAseV3FrontEndScale = "SetAseV3FrontEndScale";
        public const string SetStampDefaultSSLCertThumbprint = "setStampDefaultSSLCertThumbprint";
        public const string StampOperationPath = "stampOperation?trackingSubscription={trackingSubscription}&operationId={operationId}";

        public const string SyncItem = "syncItem";
        public const string SyncAllItems = "syncAllItems";
        public const string GetUnsyncedItems = "getUnsyncedItems";

        // This is for checking that the URL using {role} parameter is valid according to the specification
        public static readonly string[] AvailableRoles = new string[] { WebWorkerRole, LoadBalancerRole, PublisherRole, ControllerRole };
        // should not be able to execute  operations such as taking machine offline, online, restart, and repair for the controller (for on-prem)
        public static readonly string[] AvailableRolesOnPrem = new string[] { WebWorkerRole, LoadBalancerRole, PublisherRole, ManagementServerRole, FileServerRole };

        public const string PutMachineOffline = "systems/{webSystemName}/{role}/{name}/offline";
        public const string PutMachineOnline = "systems/{webSystemName}/{role}/{name}/online";
        public const string RebootMachine = "systems/{webSystemName}/{role}/{name}/reboot";
        public const string RepairMachine = "systems/{webSystemName}/{role}/{name}/repair";
        public const string MachineLog = "systems/{webSystemName}/{role}/{name}/log";
        public const string MachineLogWithDateRange = "systems/{webSystemName}/{role}/{name}/logwithdate?startTime={startTime}&cutOffTime={cutOffTime}";
        public const string IsMachineValid = "systems/{webSystemName}/{role}/{name}/valid";

        public const string DatabaseCheckAvailability = "databases/{name}?CheckAvailability";
        public const string WebSiteCheckAvailability = "sites/{name}?CheckAvailability";

        public const string QuotaManagementSubscriptionsRoot = "subscriptions";
        public const string SubscriptionIdTemplateParameter = "/{subscriptionId}";
        public const string QuotaManagementSubscriptionUsageSummary = QuotaManagementSubscriptionsRoot + SubscriptionIdTemplateParameter + "/usageSummary";
        public const string QuotaManagementQuota = "quota";
        public const string QuotaManagementQuotaValidateOnly = "quota?validateOnly={isValidateOnly}";
        public const string QuotaManagementDefaultQuota = "defaultquota";

        public const string VnetRoutes = "/routes";
        public const string VnetRouteNameTemplateParameter = "/{routeName}";

        public const string EditSubscriptionRelocationFeature = "editRelocationFeature?name={name}&limit={limit}&isBlocking={isBlocking}";
        public const string GetAutoRelocationState = "getState";
        public const string UpdateAutoRelocationState = "updateState?stateName={stateName}";
        public const string ForceSubscriptionCandidateUpdate = "forceSubscriptionCandidateUpdate";
        public const string SetSubscriptionIsKnownFeature = "setSubscpritionAsKnown?subscription={subscription}&value={value}";
        public const string SetAutoRelocationConfiguration = "SetAutoRelocationConfiguration";
        public const string GetAutoRelocationConfiguration = "GetAutoRelocationConfiguration";
        public const string GeoAdminRedisOperations = "redisOpeations";
        public const string PublicCertificatePath = "/publicCertificates";
        public const string PublicCertificateSubPath = "/publicCertificates/{publicCertificateName}";

        // Katal usage endpoint
        public const string UsageParameters = "?lastId={lastId}&batchSize={batchSize}";

        // Azure containers
        public const string CreateAzureContainer = "azurecontainers/{containerName}/create";
        public const string GetAzureContainer = "azurecontainers/{containerId}";
        public const string GetAzureContainers = "azurecontainers/allcontainers";
        public const string UpdateAzureContainer = "azurecontainers/{containerId}/update";
        public const string DeleteAzureContainer = "azurecontainers/{containerId}";
        public const string ValidateAzureContainersCache = "azurecontainers/validatecache";

        // Geo Throttling
        public const string ThrottlingApiRoot = "api";
        public const string ThrottlingApiId = "?id={id}";
        public const string ThrottlingRuleRoot = "rule";
        public const string ThrottlingRuleApiId = "?id={id}";

        // Dns Management
        public const string CNameRoot = "cname";
        public const string ARecordRoot = "arecord";
        public const string TxtRecordRoot = "txtrecord";

        // Core & VM Quota
        public const string ListVMCoreQuotas = "vmcorequota";
        public const string GetVMCoreQuota = "vmcorequota/{name}?sku={sku}";
        public const string SetVMCoreQuota = "vmcorequota";

        public const string EnableMultipleWebSpaces = "enableMultipleWebSpaces?subscriptionName={subscriptionName}&resourceGroupName={resourceGroupName}";

        public static Tuple<string, bool, bool>[] LegacyRouteRootsLegacySubscriptionLegacyWebCloud = new[] {
            new Tuple<string, bool, bool>("20120301", true, true),
            new Tuple<string, bool, bool>("20130801", false, true),
            new Tuple<string, bool, bool>("20131001", false, true)
        };

        public const string DateTimeUrlFormat = "yyyy-MM-dd-HH-mm";

        public const string InvokeWebRequestFromControllerAsync = "InvokeWebRequestFromControllerAsync";

        public static class Csm
        {
            public const string ResourceProviderName = "Microsoft.Web";
            public const string NetworkResourceProviderName = "Microsoft.Network";
            public const string AzureMonitorProviderName = "Microsoft.Insights";
            public const string ResourceProviderDisplayName = "Microsoft Web Apps";
            public const string ResourceGroupSegment = "resourceGroups";
            public const string ResourceGroupRoot = "subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}";

            public const string RegionalToGlobalRoot = "regionalToGlobal";
            public const string RegisterRegionalGeomaster = "registerRegionalGeomaster";
            public const string RegionalToGlobalResourceRoot = RegionalToGlobalRoot + "/" + ResourceRoot;
            public const string RegionalToRegionalRoot = "regionalToRegional";
            public const string RegionalToRegionalResourceRoot = RegionalToRegionalRoot + "/" + ResourceRoot;
            public const string RegionNameTemplateParameter = "/{regionName}";
            public const string SubscriptionsRoot = SystemParameter + "/" + SubscriptionResource + SubscriptionIdTemplateParameter;
            public const string ResourcesOwnershipsRoot = RegionalToGlobalRoot + "/" + SubscriptionsRoot + "/regionalOwnerships";
            public const string ResourceRoot =
                "subscriptions/{subscriptionId}/resourceGroups/{resourceGroupName}/providers/" + UriElements.Csm.ResourceProviderName;
            public const string SubcriptionResourcesFormat = RegionalToGlobalRoot + "/" + SubscriptionsRoot + "/" + SubscriptionGeoRegions + RegionNameTemplateParameter + "/resources";
            public const string UnmigratedSubcriptionResourcesFormat = RegionalToGlobalRoot + "/" + SubscriptionGeoRegions + RegionNameTemplateParameter + "/unmigratedSubscriptions";

            public const string ResourceGroupPath = "/resourceGroups/{resourceGroupName}";
            public const string SitesResource = "sites";
            public const string StaticSitesResource = "staticSites";
            public const string StaticSiteType = ResourceProviderName + "/" + StaticSitesResource;
            public const string StaticSitesResourcePrefix = StaticSiteType + "/";
            public const string StaticSitesConfigPrefix = "/config";
            public const string StaticSitesFunctionAppSettingsResource = StaticSitesConfigPrefix + StaticSitesFunctionAppSettingsSuffix;
            public const string StaticSitesFunctionAppSettingsSuffix = "/functionappsettings";
            public const string StaticSitesListFunctionAppSettings = "/listFunctionAppSettings";
            public const string StaticSiteAppSettingNameTemplateParamater = "/{appsettingname}";
            public const string StaticSiteBuilds = "/builds";
            public const string StaticSiteDetach = "/detach";
            public const string StaticSitesFunctions = "/functions";
            public const string StaticSiteFunctionType = StaticSiteType + StaticSitesFunctions;
            public const string StaticSitesAdmin = "/admin";
            public const string StaticSitesAdminRoot = StaticSitesResource + StaticSitesAdmin;
            public const string StaticSitesBuildResourceRoot = StaticSitesResource + StaticSiteBuilds;
            public const string StaticSiteBuildType = StaticSiteType + StaticSiteBuilds;
            public const string StaticSiteBuildResourcePrefix = StaticSiteBuilds + "/";
            public const string StaticSiteVersionChangeLiteral = "changeStaticSiteInternalVersion";
            public const string StaticSiteDeleteStorageContainersLiteral = "deleteStorageContainers";
            public const string StaticSiteBackfillContentLiteral = "backfillContent";
            public const string StaticSiteBlueRidgePrefix = "blueRidge";
            public const string StaticSiteOnboardBlueRidgeAccount = "/onboardAccount";
            public const string StaticSiteOnboardBlueRidgeSubscription = "/onboardSubscription";
            public const string StaticSiteProvisionBlueRidgeResourceGroup = "/provisionResourceGroup";
            public const string StaticSiteGetBlueRidgeJobInfo = "/getJobInfo";
            public const string StaticSiteCustomDomains = "/" + StaticSitesCustomDomainsName;
            public const string StaticSitesCustomDomainsName = "customDomains";
            public const string StaticSiteCustomDomainTemplateParamater = "/{domainName}";
            public const string StaticSiteCustomDomainType = StaticSiteType + StaticSiteCustomDomains;
            public const string StaticSiteCustomDomainResourcePrefix = StaticSiteCustomDomains + "/";
            public const string StaticSiteUsers = "/users";
            public const string StaticSiteListUsers = "/listUsers";
            public const string StaticSiteCreateUserInvitation = "/createUserInvitation";
            public const string StaticSiteInvitations = "/" + StaticSiteInvitationsLiteral;
            public const string StaticSiteInvitationsLiteral = "invitations";
            public const string StaticSiteInvitationType = StaticSiteType + StaticSiteInvitations;
            public const string StaticSiteUserType = StaticSiteType + StaticSiteUsers;
            public const string StaticSiteUserResourcePrefix = StaticSiteUsers + "/";
            public const string StaticSiteAuthProviders = "/authproviders";
            public const string StaticSiteAuthProviderTemplateParamater = "/{authprovider}";
            public const string StaticSiteUserIdTemplateParamater = "/{userid}";
            public const string StaticSiteConfiguredRolesType = StaticSiteType + StaticSiteConfiguredRolesBase;
            public const string StaticSiteConfiguredRolesBase = "/" + StaticSiteConfiguredRolesLiteral;
            public const string StaticSiteConfiguredRolesLiteral = "configuredRoles";
            public const string StaticSiteListConfiguredRoles = "/listConfiguredRoles";
            public const string StaticSitesSecretType = StaticSiteType + StaticSitesSecretsBase;
            public const string StaticSitesSecretsLiteral = "secrets";
            public const string StaticSitesSecretsBase = "/" + StaticSitesSecretsLiteral;
            public const string StaticSitesListSecrets = "/listSecrets";
            public const string StaticSiteResetApiKey = "/resetapikey";
            public const string StaticSiteCustomDomainValidateSuffix = "/validate";
            public const string PrIdTemplateParamater = "/{prId}";
            public const string DomainResourceNameTemplateParamater = "/{domainResourceName}";
            public const string Functions = "functions";
            public const string FunctionsResource = "functions/{*functionApiMethod}";
            public const string FunctionsAdminTokenResource = "/functions/admin/token";
            public const string ListSecrets = "/listSecrets";
            public const string DeletedSitesResource = "deletedSites";
            public const string DeletedSiteIdParameter = "/{deletedSiteId}";
            public const string WebQuotasResource = "webquotas";
            public const string WebAdminResource = "webadmin";
            public const string SubscriptionResource = "subscriptions";
            public const string NameTemplateParamater = "/{name}";
            public const string NameTemplateParamaterName = "name";
            public const string NameTemplateParamWithConstraint = "/{name:regex(^[^()]+$)}";
            public const string SiteNameTemplateParameter = "/{siteName}";
            public const string BindingNameTemplateParameter = "/{bindingName}";
            public const string HostingEnvironmentNameTemplateParameter = "/{hostingEnvironmentName}";
            public const string RecommendationResource = "/recommendations";
            public const string RecommendationsOperationResource = "recommendations";
            public const string RecommendationHistoryResource = "/recommendationHistory";
            public const string RecommendationIdResource = "/{recommendationId}";
            public const string SlotNameTemplateParameter = "/{slot}";
            public const string SlotTemplateParamWithConstraint = "/{slot:regex(^[^()]+$)}";
            public const string SlotsResource = "/slots";
            public const string SiteInstancesResource = "/instances";
            public const string SiteInstanceIdResource = "/{instanceId}";
            public const string HealthStatusResource = "/healthstatus";
            public const string SiteConfigResource = "/config/web";
            public const string SiteConfigCollection = "/config";
            public const string SiteConfigSnapshotResource = "config/snapshot";
            public const string SitePhpLogSettingsResource = "/phplogging";
            public const string ResourceHealthMetadata = "/resourceHealthMetadata";
            public const string ResourceHealthMetadataResource = "resourceHealthMetadata";
            public const string ResourceHealthMetadataResourceName = "default";
            public const string SiteSourceControlsResource = "/sourcecontrols/web";
            public const string SitePremierAddOnsResource = "/premieraddons";
            public const string SitePremierAddOnNameResource = "/{premierAddOnName}";
            public const string SitePremierAddOnOffersResource = "premieraddonoffers";
            public const string SiteAppSettingsResource = "/config/appsettings";
            public const string SiteAppSettingsResourceName = "appsettings";
            public const string SiteConfigReferenceAppSettingsResource = "/appsettings";
            public const string SiteConfigReferenceAppSettingKeyResource = "/{appSettingKey}";
            public const string SiteConnectionStringsResource = "/config/connectionstrings";
            public const string SiteConnectionStringsResourceName = "connectionstrings";
            public const string SiteAzureStorageAccountsResource = "/config/azurestorageaccounts";
            public const string SiteAzureStorageAccountsResourceName = "azurestorageaccounts";
            public const string SiteAuthSettingsResource = "/config/authsettings";
            public const string SiteAuthSettingsV2Resource = "/config/authsettingsV2";
            public const string SiteAuthSettingsResourceName = "authsettings";
            public const string SiteAuthSettingsV2ResourceName = "authsettingsV2";
            public const string SitePublishingCredentialsResource = "/config/publishingcredentials";
            public const string SitePublishingCredentialsResourceName = "publishingcredentials";
            public const string SiteBasicPublishingCredentialsPolicies = "/basicpublishingcredentialspolicies";
            public const string SiteFtpAllowedResource = "/ftp";
            public const string SiteScmAllowedResource = "/scm";
            public const string SitePushSettingsResource = "/config/pushsettings";
            public const string SitePushSettingsResourceName = "pushsettings";
            public const string SiteMetadataResource = "/config/metadata";
            public const string SiteMetadataResourceName = "metadata";
            public const string SiteLogsConfigResource = "/config/logs";
            public const string SiteLogsConfigResourceName = "logs";
            public const string SiteUsagesResource = "/usages";
            public const string SiteMetricsResource = "/metrics";
            public const string SiteMetricDefinitionsResource = "/metricdefinitions";
            public const string SiteConfigReferenceResource = "/config/configreferences";
            public const string MetricDefinitionsResource = "/metricDefinitions";
            public const string MetricsResource = "/metrics";
            public const string SitePerfMonResource = "/perfcounters";
            public const string PublishingProfileResource = "/publishxml";
            public const string SiteRestartAction = "/restart";
            public const string SiteStartAction = "/start";
            public const string SiteStopAction = "/stop";
            public const string SnapshotsResource = "/snapshots";
            public const string SnapshotsResourceFromDR = "/snapshotsdr";
            public const string SnapshotIdResource = "/{snapshotId}";
            public const string RestorableSnapshotsResource = "/restorableSnapshots";
            public const string SyncSiteRepositoryAction = "/sync";
            public const string GenerateNewPasswordAction = "/newpassword";
            public const string IsValidCustomDomain = "/isvalidcustomdomain";
            public const string AnalyzeCustomHostname = "/analyzeCustomHostname";
            public const string SiteSwapAction = "/slotsswap";
            public const string SiteSlotCopyAction = "/slotcopy";
            public const string SiteSlotDifferences = "/slotsdiffs";
            public const string SiteSlotApplySlotConfig = "/applySlotConfig";
            public const string SiteSlotResetSlotConfig = "/resetSlotConfig";
            public const string SiteSlotConfigNames = "/config/slotConfigNames";
            public const string OperationResultsTemplate = "locations/{location}/operationResults";
            public const string OperationsInLocationTemplate = "locations/{location}/operations";
            public const string OperationIdParameter = "/{operationId}";
            public const string OperationsResource = "/operations/{operationId}";
            public const string ResourceGroupOperationsResource = "operations/{operationId}";
            public const string RulesResource = "/rules";
            public const string SiteRecoverAction = "/recover";
            public const string RestoreSnapshotAction = "/restoreSnapshot";
            public const string RestoreDeletedAppAction = "/restoreFromDeletedApp";
            public const string ServerFarmRestartSitesAction = "/restartSites";
            public const string MoveResourcesAction = "moveResources";
            public const string ValidateMoveResourcesAction = "validateMoveResources";
            public const string ResetMoveResourcesAction = "/resetMoveResources";
            public const string DeploymentsResourceName = "deployments";
            public const string DeploymentsResourceParameter = "{deploymentName}";
            public const string PreflightAction = "/preflight";
            public const string ClassicMobileServicesResource = "classicMobileServices";
            public const string RecommendationDisableAction = "/disable";
            public const string RecommendationSnoozeAction = "/snooze";
            public const string RecommendationFilterUpdateAction = "/update";
            public const string RecommendationFilterResetAction = "/reset";
            public const string RecommendationAnalyticsResource = "/analytics";

            public const string DiagnosticCategoryTemplateParameter = "/{diagnosticCategory}";
            public const string Availability = "/availability";
            public const string SiteDiagnostics = "/diagnostics";
            public const string Troubleshoot = "/troubleshoot";
            public const string Analyses = "/analyses";
            public const string AnalysisTemplateParameter = "/{analysisName}";
            public const string AseAvailabilityAnalysis = "/aseAvailabilityAnalysis";
            public const string AseDeploymentAnalysis = "/aseDeploymentAnalysis";
            public const string AppAnalysis = "/appAnalysis";
            public const string PerfAnalysis = "/perfAnalysis";
            public const string Detectors = "/detectors";
            public const string DetectorsType = "detectors";
            public const string Gists = "/gists";
            public const string Insights = "/insights";
            public const string SiteDiagnosticsProperties = "/diagnosticProperties";
            public const string DetectorTemplateParameter = "/{detectorName}";
            public const string GistTemplateParameter = "/{gistName}";
            public const string Execute = "/execute";
            public const string DiagnosticQuery = "/query";
            public const string Invoke = "/invoke";

            public const string SuspendEnvironment = "/suspend";
            public const string ResumeEnvironment = "/resume";
            public const string Diagnostics = "/diagnostics";
            public const string DiagnosticsNameTemplateParamater = "/{diagnosticsName}";

            public const string BackupResource = "/backup";
            public const string BackupResourceName = "backups";
            public const string BackupResources = "/backups";
            public const string ListBackupsAction = "/listbackups";
            public const string BackupConfigResource = "/config/backup";
            public const string BackupStatusResource = "/backups/{backupId}";
            public const string RestoreResource = "/restore";
            public const string RestoreFromBackupBlobAction = "/restoreFromBackupBlob";
            public const string DiscoverBackupAction = "/discoverbackup";
            public const string RestoreDiscoverResource = "/restore/discover";
            public const string DiscoverResource = "/discover";
            public const string ValidateResource = "/validate";
            public const string ContainerResource = "container";

            public const string SyncFunctionTriggers = "/syncfunctiontriggers";
            public const string SyncFunctions = "/sync";
            public const string ListSyncFunctionTriggerStatus = "/listsyncfunctiontriggerstatus";
            public const string ListSyncStatus = "/listsyncstatus";
            public const string SnapshotResource = "snapshots";
            public const string SnapshotResourceType = ResourceProviderName + "/" + SitesResource + "/" + SnapshotResource;
            public const string DeletedSiteSnapshotResourceType = ResourceProviderName + "/" + DeletedSitesResource + "/" + SnapshotResource;
            public const string PerRegionDeletedSiteSnapshotResourceType = ResourceProviderName + "/locations/" + DeletedSitesResource + "/" + SnapshotResource;
            public const string SiteConfigSnapshotResourceType = ResourceProviderName + "/" + SitesResource + "/" + SiteConfigSnapshotResource;

            public const string MigrateResource = "/migrate";
            public const string MigrateMySqlResource = "/migratemysql";
            public const string MigrateMySqlStatusResource = "/status";

            public const string ChangeVnet = "/changeVirtualNetwork";
            public const string SyncVnet = "/syncVirtualNetwork";

            public const string AzureMonitorResourceProvider = "/providers/" + AzureMonitorProviderName;
            public const string AzureMonitorDiagnosticSettingName = "/diagnosticSettings";
            public const string AzureMonitorDiagnosticSettingParameter = "/{diagnosticSetting}";

            public const string MicrosoftWebLinkedNotify = "/providers/" + ResourceProviderName + "/notify";

            // Deprecated backup/restore URI elements
            public const string BackupConfigResourceDeprecated = "/backup/config";
            public const string ResourceGroupFormat = "/subscriptions/{0}/resourceGroups/{1}";

            public const string RootProviderResourceFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName;

            public const string SiteFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}";

            public const string SiteOperationResultsFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}/operationresults/{3}";

            public const string SiteWithSlotFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}/slots/{3}";

            public const string SiteResourceNameFormat = "{0}/{1}";
            public const string SiteResourceNameOldFormat = "{0}({1})";

            public const string SiteWithSlotOperationResultsFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}/slots/{3}/operationresults/{4}";

            public const string ResourceGroupOperationFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/operations/{2}";

            public const string HostingEnvironmentOperationFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + HostingEnvironments + "/{2}/operations/{3}";

            public const string ServerFarmFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/serverFarms/{2}";

            public const string ServerFarmOperationFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/serverFarms/{2}/" + OperationResultsRestOnlyResource + "/{3}";

            public const string SiteNetworkTraceOperationResultsFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}/" + NetworkTracesResource + "/current/operationresults/{3}";

            public const string SiteWithSlotNetworkTraceOperationResultsFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/sites/{2}/slots/{3}/" + NetworkTracesResource + "/current/operationresults/{4}";

            public const string CertificateFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/" + CertificatesResource + "/{2}";

            public const string HostingEnvironmentsFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName + "/" + HostingEnvironmentsResource + "/{2}";

            public const string GlobalResourceRoot =
                "subscriptions/{subscriptionId}/providers/" + UriElements.Csm.ResourceProviderName;

            public const string SubscriptionResourceRoot =
                "subscriptions/providers/" + UriElements.Csm.ResourceProviderName;

            public const string ProviderResourceRoot =
                "providers/" + UriElements.Csm.ResourceProviderName;

            public const string OnPremProviderResourceRoot =
                "subscriptions/{subscriptionId}/providers/" + UriElements.Csm.ResourceProviderName;

            public const string EventGridFilterResourceFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName +
                "/serverFarms/{2}/EventGridFilters/{3}";

            public const string EventGridFilterNameFormat = "{0}/{1}";

            public const string VnetRouteResourceFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName +
                "/serverFarms/{2}/virtualNetworkConnections/{3}/routes/{4}";

            public const string VnetGatewayResourceFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName +
                "/serverFarms/{2}/virtualNetworkConnections/{3}/gateways/{4}";

            public const string RecommendationRuleResource = GlobalResourceRoot + RecommendationResource;
            public const string RecommendationSiteDetailResource = SiteFormat + RecommendationResource + NameParameter;
            public const string RecommendationHostingEnvironmentDetailResource = HostingEnvironmentsFormat + RecommendationResource + NameParameter;

            public const string RecommendationServerFarmDetailResource =
                ServerFarmFormat + RecommendationResource + NameParameter;

            public const string RecommendationSiteRuleResource = SiteFormat + RecommendationResource;
            public const string RecommendationHistorySiteResource = SiteFormat + RecommendationHistoryResource;

            public const string RecommendationServerFarmRuleResource = ServerFarmFormat + RecommendationResource;
            public const string RecommendationHistoryServerFarmResource = ServerFarmFormat + RecommendationHistoryResource;

            public const string RecommendationHostingEnvironmentRuleResource = HostingEnvironmentsFormat + RecommendationResource;
            public const string RecommendationHistoryHostingEnvironmentResource = HostingEnvironmentsFormat + RecommendationHistoryResource;

            public const string RecommenationEngineResourceRoot = "/api/engine";
            public const string RecommenationEngineRunResource = RecommenationEngineResourceRoot + "/run";

            public const string RecommenationEngineOperationResource =
                RecommenationEngineResourceRoot + "/requests" + OperationIdParameter;

            public const string RecommenationEngineRuleResource = RecommenationEngineResourceRoot + "/rules";
            public const string RecommenationFilterResourceRoot = "/api/filter";

            public const string RecommenationFilterSubscriptionResource =
                RecommenationFilterResourceRoot + "/subscriptions";

            public const string RecommenationEngineToastResource = RecommenationEngineResourceRoot + "/subscriptions/{subscriptionId}/toast";

            public const string Capabilities = "/capabilities";

            public const string DiagnosticApplicationAnalysis = SiteFormat + SiteDiagnostics + AppAnalysis;
            public const string DiagnosticPerformanceAnalysis = SiteFormat + SiteDiagnostics + PerfAnalysis;
            public const string DiagnosticsGetDetectors = SiteFormat + SiteDiagnostics + Detectors;
            public const string DiagnosticsGetDetectorResourceFormat = SiteFormat + SiteDiagnostics + Detectors + "/{3}";
            public const string DiagnosticGetSiteProperties = SiteFormat + SiteDiagnostics + SiteDiagnosticsProperties;

            public const string HybridConnection = "/hybridconnection";
            public const string HybridConnectionEntityNameTemplateParameter = "/{entityName}";
            public const string HybridConnectionRelays = "/hybridConnectionRelays";
            public const string HybridConnectionRelayFullPath = "/hybridConnectionNamespaces/{namespaceName}/relays/{relayName}";
            public const string HybridConnectionSites = "/sites";
            public const string HybridConnectionPlanLimits = "/hybridConnectionPlanLimits/limit";
            public const string HybridConnectionListKeys = "/listKeys";

            public const string PrivateAccessUri = "/privateAccess/virtualNetworks";

            public const string SwiftVnetConnection = "/config/virtualNetwork";
            public const string NetworkConfigSwiftVnetConnection = "/networkConfig/virtualNetwork";
            public const string VnetConnection = "/virtualNetworkConnections";
            public const string VnetConnectionNameTemplateParameter = "/{vnetName}";
            public const string VnetGateway = "/gateways";
            public const string VnetGatewayNameTemplateParameter = "/{gatewayName}";

            public const string NetworkFeatures = "/networkFeatures";
            public const string NetworkFeaturesNameTemplateParameter = "/{view}";

            public const string NetworkTraceResource = "networkTrace";
            public const string NetworkTracesResource = "networkTraces";

            public const string ServerFarmsResource = "serverfarms";
            public const string ServerFarmNameTemplateParameter = "/{serverFarmName}";
            public const string WebHostingPlansResource = "webhostingplans";
            public const string ServerFarmPerformanceResource = "/performance";
            public const string ServerFarmUsageResource = "/usages";
            public const string ServerFarmMetricsResource = "/metrics";
            public const string ServerFarmMetricDefinitionsResource = "/metricdefinitions";
            public const string ServerFarmVirtualNetworkConnections = "/virtualNetworkConnections";
            public const string ServerFarmVnetNameTemplateParameter = "/{vnetName}";
            public const string ServerFarmVnetRoutes = "/routes";
            public const string ServerFarmVnetRoutesNameTemplateParameter = "/{routeName}";

            public const string UsersResource = "users";
            public const string IsSiteNameAvailable = "/issitenameavailable/";
            public const string TryAddSiteName = "/tryaddsitename";
            public const string DeleteSiteName = "/deletesitename";
            public const string DeleteAseSiteNames = "/deleteasesitenames";
            public const string NamespaceDescriptorParameter = "/{namespaceDescriptor}";
            public const string TryUpdateArmResourceIdForSiteName = "/TryUpdateArmResourceIdForSiteName";
            public const string GetArmResourceIdForSiteName = "/GetArmResourceIdForSiteName/";

            public const string IsAseNameAvailable = "/isasenameavailable";
            public const string TryAddAseName = "/tryaddasename";
            public const string DeleteAseName = "/deleteasename";

            public const string CertificatesResource = "certificates";
            public const string CertificatesResourceType = ResourceProviderName + "/" + CertificatesResource;
            public const string CsrsResource = "csrs";

            public const string HostingEnvironmentsResource = "hostingEnvironments";
            public const string InboundDependencies = "inboundDependencies";
            public const string OutboundDependencies = "outboundDependencies";

            public const string HostingEnvironments = "/hostingEnvironments";

            public const string HostingEnvironmentsOperationsResource =
                HostingEnvironmentsResource + NameTemplateParamater + "/operations/{operationId}";

            public const string HostingEnvironmentsListOperationsResource =
                HostingEnvironmentsResource + NameTemplateParamater + "/operations";

            public const string HostingEnvironmentMultiRolesResource = "multiRolePools";
            public const string HostingEnvironmentWorkerPoolsResource = "workerPools";
            public const string HostingEnvironmentDiagnosticsResource = "diagnostics";

            public const string HostingEnvironmentDefaultMultiRolePool = "default";

            public const string HostingEnvironmentMultiRolePool =
                HostingEnvironmentsResource + NameTemplateParameter + "/" + HostingEnvironmentMultiRolesResource + "/" +
                HostingEnvironmentDefaultMultiRolePool;

            public const string HostingEnvironmentWorkerPool =
                HostingEnvironmentsResource + NameTemplateParamater + "/" + HostingEnvironmentWorkerPoolsResource +
                "/{workerPoolName}";

            public const string HostingEnvironmentInstance = "/instances/{instance}";

            public const string HostingEnvironmentMetricDefinitionsResource = "/metricdefinitions";
            public const string HostingEnvironmentMetricsResource = "/metrics";

            public const string HostingEnvironmentMetrics =
                HostingEnvironmentsResource + NameTemplateParamater + HostingEnvironmentMetricsResource;

            public const string HostingEnvironmentMetricDefinitions =
                HostingEnvironmentsResource + NameTemplateParamater + HostingEnvironmentMetricDefinitionsResource;

            public const string HostingEnvironmentUsages =
                HostingEnvironmentsResource + NameTemplateParamater + "/usages";

            public const string HostingEnvironmentsMultiRoleMetrics =
                HostingEnvironmentMultiRolePool + HostingEnvironmentMetricsResource;

            public const string HostingEnvironmentsMultiRoleMetricDefinitions =
                HostingEnvironmentMultiRolePool + HostingEnvironmentMetricDefinitionsResource;

            public const string HostingEnvironmentMultiRoleUsages = HostingEnvironmentMultiRolePool + "/usages";

            public const string HostingEnvironmentsWebWorkerMetrics =
                HostingEnvironmentWorkerPool + HostingEnvironmentMetricsResource;

            public const string HostingEnvironmentsWebWorkerMetricDefinitions =
                HostingEnvironmentWorkerPool + HostingEnvironmentMetricDefinitionsResource;

            public const string HostingEnvironmentWebWorkerUsages = HostingEnvironmentWorkerPool + "/usages";

            public const string HostingEnvironmentComputeCapacity =
                HostingEnvironmentsResource + NameTemplateParamater + "/capacities/compute";

            public const string HostingEnvironmentVipCapacity =
                HostingEnvironmentsResource + NameTemplateParamater + "/capacities/virtualip";

            public const string HostingEnvironmentHealth =
                HostingEnvironmentsResource + NameTemplateParameter + HealthPath;
            public const string Health = "health";
            public const string HealthPath = "/" + Health;
            public const string Default = "default";
            public const string DefaultPath = "/" + Default;

            public const string ManagedConnectionsResource = "connections";
            public const string ManagedConnectionGatewaysResource = "connectionGateways";
            public const string CustomApisResource = "customApis";

            public const string IsHostNameAvailable = "ishostnameavailable";
            public const string IsHostNameReservedOrNotAllowed = "ishostnamereservedornotallowed";
            public const string SiteExists = "siteExists";
            public const string LegacySubDomainParam = "/{subDomain}";
            public const string LegacyNameParam = "/{name}";
            public const string IsPublishingUserNameAvailable = "isusernameavailable";
            public const string IsHostingEnvironmentNameAvailable = "ishostingenvironmentnameavailable";
            public const string CheckNameAvailability = "checknameavailability";

            public const string SubscriptionPublishingUsers = "publishingUsers";
            public const string SubscriptionPublishingCredentials = "publishingCredentials";
            public const string SubscriptionGeoRegions = "geoRegions";
            public const string DeploymentLocations = "deploymentLocations";
            public const string SubscriptionHostNames = "hostNames";
            public const string HostNameBindings = "hostNameBindings";
            public const string HostNameTemplateParameterName = "hostName";
            public const string HostNameTemplateParameter = "{" + HostNameTemplateParameterName + "}";
            public const string HostNameParameter = "/{hostname}";
            public const string EventGridFilterNameTemplateParameterName = "eventGridFilterName";
            public const string EventGridFilters = "eventGridFilters";
            public const string EventGridFilterNameTemplateParameter = "{" + EventGridFilterNameTemplateParameterName + "}";
            public const string EventGridParameter = "eventGrid";
            public const string SubscriptionVerifyTrafficManagerConfiguration = "verifyTrafficManagerConfiguration";
            public const string VerifyHostingEnvironmentVnet = "verifyHostingEnvironmentVnet";
            public const string CollectVnetConfiguration = "collectVnetConfiguration";
            public const string ListSitesAssignedToHostNameActionName = "listSitesAssignedToHostName";

            public const string ProvisionGlobalAppServicePrincipalInUserTenantActionName =
                "provisionGlobalAppServicePrincipalInUserTenant";

            public const string SubscriptionUnregisterTrafficManagerConfiguration =
                "unregisterTrafficManagerConfiguration";

            public const string ConfirmTrafficManagerConfiguration = "confirmTrafficManagerConfiguration";
            public const string DnsSuffix = "dnsSuffix";
            public const string Webquotas = "plans/{planName}/webQuotas";
            public const string WebquotasResourceType = "webQuotas";

            public const string PublishingUsersResource = "publishingUsers/web";

            public const string CsmSubscriptionsResourcesRoot = "csm_subscriptions";
            public const string SubscriptionsResourcesRootAddition = "providers/" + UriElements.Csm.ResourceProviderName;
            public const string MigrateSubscriptionParameter = "/migratesubscription";

            public const string SiteExtensionResourceType = "extensions";
            public const string SiteExtensionResource = "/extensions/{*extensionApiMethod}";

            public const string MSDeployExtensionResourceType = "MSDeploy";
            public const string MSDeployExtensionResource = "/extensions/MSDeploy";
            public const string MSDeployLogExtensionResourceType = "MSDeploy/log";
            public const string MSDeployLogExtensionResource = "/extensions/MSDeploy/log";

            public const string WebSiteContainerLogsZipResource = "/containerlogs/zip";
            public const string WebSiteContainerLogsZipDownloadResource = "/containerlogs/zip/download";
            public const string WebSiteContainerLogsResource = "/containerlogs";

            public const string HostRuntimeApis = "hostruntime";
            public const string HostRuntimeApiResource =
                "/{hostRuntimeApiName:regex(^(" + HostRuntimeApis + ")$)}/{*extensionApiMethod}";

            public const string KuduApis = "processes|webjobs|triggeredwebjobs|continuouswebjobs|deployments|siteextensions|functions|scan";
            public const string KuduApiResource =
                "/{kuduApiName:regex(^(" + KuduApis + ")$)}/{*extensionApiMethod}";
            public const string KuduApiId = "/{id}";
            public const string KuduApiStart = "/start";
            public const string KuduApiStop = "/stop";
            public const string KuduApiRun = "/run";

            public const string KuduApiProcesses = "/processes";
            public const string KuduApiProcessIdResource = "/{processId}";
            public const string KuduApiThreads = "/threads";
            public const string KuduApiThreadIdResource = "/{threadId}";
            public const string KuduApiModules = "/modules";
            public const string KuduApiModuleBaseAddressResource = "/{baseAddress}";
            public const string KuduApiDump = "/dump";

            public const string KuduApiDeploymentsResource = "/deployments";
            public const string KuduApiDeploymentResource = "/deployments/{id}";
            public const string KuduApiDeploymentLogResource = "/deployments/{id}/log";

            public const string KuduApiContinuousWebJobs = "/continuouswebjobs";
            public const string KuduApiTriggeredWebJobs = "/triggeredwebjobs";
            public const string KuduApiTriggeredWebJobHistory = "/history";
            public const string KuduApiWebJobs = "/webjobs";
            public const string KuduApiWebJobNameResource = "/{webJobName}";

            public const string KuduApiSiteExtensions = "/siteextensions";
            public const string KuduApiSiteExtensionResource = "/{siteExtensionId}";

            public const string KuduApiFunctions = "/functions";
            public const string KuduApiHost = "/host/default";
            public const string KuduApiFunctionNameResource = "/{functionName}";
            public const string KuduApiKeyNameResource = "/{keyName}";
            public const string KuduApiListSecrets = "/listsecrets";
            public const string ListKeysAction = "/listkeys";
            public const string KuduApiHostKeyType = "/{keyType:regex(^(functionkeys|systemkeys)$)}";
            public const string KuduApiFunctionKeys = "/functionkeys";
            public const string KuduApiKeys = "/keys";
            public const string KuduApiStatus = "/status";
            public const string KuduApiProperties = "/properties";
            public const string KuduApiBindings = "/bindings";
            public const string KuduApiTemplates = "/templates";
            public const string KuduApiConfig = "/config";
            public const string KuduApiState = "/state";

            public const string StacksResource = "stacks";
            public const string RuntimesResource = "runtimes";
            public const string ListAction = "/list";

            public const string PricingTiersResource = "pricingTiers";
            public const string SubscriptionsWithQuotas = "quotaSettings";
            public const string MetaData = "metadata";

            public const string SourceControlsResource = "sourcecontrols";
            public const string SourceControlTypeParam = "/{sourceControlType}";
            public const string LiveSiteAnalysis = "LiveSiteAnalysis";
            public const string LiveSiteReport = "LiveSiteReport";
            public const string LiveSiteId = "/{id}";
            public const string LiveSiteFiles = "/files";
            public const string LiveSiteFileName = "/{fileName}";
            public const string SubscriptionId = "{subscriptionId}";
            public const string WorkerName = "/workers/{workerName}";

            // web admin service
            public const string Stamp = "environments";
            public const string StampFeatures = "features";
            public const string TurnStampFeatureOff = "/off";
            public const string TurnStampFeatureOn = "/on";
            public const string SystemParameter = "systems/{webSystemName}";
            public const string Summary = "/summary";
            public const string Config = "/config";
            public const string IpBlocklist = "ipBlocklists";
            public const string V4 = "V4";
            public const string V6 = "V6";
            public const string Enabled = "Enabled";
            public const string Enable = "Enable";
            public const string Disable = "Disable";
            public const string AuditLogs = "/auditlogs";
            public const string Credentials = "credentials";
            public const string Log = "/log";
            public const string LogWithStartAndEnd = "/log/{startTime}/endTime/{endTime}";
            public const string MachineLog = "systems/{webSystemName}/{role}/{name}/log";
            public const string FilteredServers = "roles/{roleId}";

            public const string FilteredServersWithWorker =
                "roles/{roleId}/workerSize/{workerSizeId}/computeMode/{computeMode}";

            public const string TraceMessages = "/traceMessages";
            public const string Offline = "/offline";
            public const string Online = "/online";
            public const string Reboot = "/reboot";
            public const string RemoveRunningSitesFromWorker = "/removeRunningSites";
            public const string Repair = "/repair";
            public const string Sites = "/sites";
            public const string ServerFarms = "/serverfarms";
            public const string WebHostingPlans = "/webhostingplans";
            public const string Valid = "/valid";
            public const string ValidRoleServer = "validateRoleServer";
            public const string WorkerSanitizingAllDrives = "/workerSanitizingAllDrives";
            public const string SpareWebWorkers = "/spareWebWorkers";
            public const string WorkerSanitizingSuspended = "/workerSanitizingSuspended";
            public const string Skus = "/skus";
            public const string WorkerTierParameter = "/workerSize/{workerSize}/computeMode/{computeMode}";
            public const string CustomProductParameter = "/{productId}";
            public const string BillingMetersResource = "billingMeters";

            //Domain manager constants
            public const string DomainsResource = "domains";
            public const string DomainNameTemplateParameterName = "domainName";
            public const string DomainNameTemplateParameter = "/{" + DomainNameTemplateParameterName + "}";
            public const string TopLevelDomainRestOnlyResource = "topLevelDomains";
            public const string DomainOwnershipIdentifierResource = "domainownershipidentifiers";
            public const string DomainSearchesRestOnlyResource = "domainSearches";
            public const string OperationResultsRestOnlyResource = "operationresults";
            public const string ListTopLevelDomainAgreementAction = "listAgreements";
            public const string DomainAvailablityCheckAction = "checkDomainAvailability";
            public const string ValidateDomainRegistrationInformationAction = "validateDomainRegistrationInformation";
            public const string DomainRecommendationAction = "listDomainRecommendations";
            public const string DomainRenewAction = "/renew";
            public const string generateSsoRequestAction = "generateSsoRequest";
            public const string DomainRegistrationResourceProviderName = "Microsoft.DomainRegistration";
            public const string DomainRegistrationResourceProviderDisplayName = "Microsoft Domains";
            public const string SharedResourceProviderBaseUriPart = "sharedResourceProviderBase";
            public const string ResourceGroups = "resourceGroups";
            public const string Providers = "providers";
            public const string SubscriptionIdTemplateParameterName = "subscriptionId";
            public const string ResourceGroupNameTemplateParameterName = "resourceGroupName";
            public const string ResourceNameTemplateParameterName = "resourceName";
            public const string SubResourceNameTemplateParameterName = "subResourceName";
            public const string GlobalLocation = "global";
            public const string ResourceGroupNameTemplateParameter = "/{" + ResourceGroupNameTemplateParameterName + "}";

            public const string DomainOperationFormat =
                "/" + Subscriptions + "/{0}/" + ResourceGroups + "/{1}/" + Providers + "/" +
                DomainRegistrationResourceProviderName + "/" + DomainsResource + "/{2}/" +
                OperationResultsRestOnlyResource + "/{3}";

            public const string DomainRegistrationUriPart = "domainRegistration";
            public const string AdminDomainRegistrationUriPart = "adminDomainRegistration";

            public const string DomainRegistrationResourceProviderBasePath =
                SharedResourceProviderBaseUriPart + "/" + DomainRegistrationUriPart;

            public const string AdminDomainRegistrationResourceProviderBasePath =
                SharedResourceProviderBaseUriPart + "/" + AdminDomainRegistrationUriPart;

            public const string DomainRegistrationResourceRoot =
                Subscriptions + SubscriptionIdTemplateParameter + "/" + ResourceGroups +
                ResourceGroupNameTemplateParameter + "/" + Providers + "/" + DomainRegistrationResourceProviderName;

            public const string DomainRegistrationResourceRootUri =
                DomainRegistrationResourceProviderBasePath + "/" + DomainRegistrationResourceRoot;

            public const string GlobalDomainRegistrarResourceRoot =
                "/" + Subscriptions + SubscriptionIdTemplateParameter + "/" + Providers + "/" +
                DomainRegistrationResourceProviderName;

            public const string GlobalDomainRegistrarResourceRootUri =
                DomainRegistrationResourceProviderBasePath + GlobalDomainRegistrarResourceRoot;

            public const string DomainRegistrationProviderResourceRoot =
                DomainRegistrationResourceProviderBasePath + "/providers/" +
                UriElements.Csm.DomainRegistrationResourceProviderName;

            public const string DomainResourceIdTemplate = "/" + UriElements.Csm.DomainRegistrationResourceRoot + "/" + UriElements.Csm.DomainsResource + UriElements.Csm.DomainNameTemplateParameter;
            public const string DomainRestoreHardDeletedDomain = "restoreHardDeletedDomain";
            public const string DomainPurchaseDomainPrivacy = "purchaseDomainPrivacy";
            public const string DomainRetrieveResellerDomain = "retrieveResellerDomain";
            public const string DomainMoveDomainDb = "moveDomainDb";
            public const string DomainUpdateResellerOwner = "updateResellerOwner";

            public const string CertificateRegistrationResourceProviderName = "Microsoft.CertificateRegistration";
            public const string CertificateRegistrationResourceProviderDisplayName = "Microsoft Certificates";
            public const string CertificateRegistrationUriPart = "certificateRegistration";
            public const string AdminCertificateRegistrationUriPart = "adminCertificateRegistration";

            public const string CertificateRegistrationResourceProviderBasePath =
                SharedResourceProviderBaseUriPart + "/" + CertificateRegistrationUriPart;

            public const string AdminCertificateRegistrationResourceProviderBasePath =
                SharedResourceProviderBaseUriPart + "/" + AdminCertificateRegistrationUriPart;

            public const string CertificateRegistrationResourceRoot =
                Subscriptions + SubscriptionIdTemplateParameter + "/" + ResourceGroups +
                ResourceGroupNameTemplateParameter + "/" + Providers + "/" + CertificateRegistrationResourceProviderName;

            public const string CertificateRegistrationRootUri =
                CertificateRegistrationResourceProviderBasePath + "/" + CertificateRegistrationResourceRoot;

            public const string GlobalCertificateRegistrationResourceRoot =
                "/" + Subscriptions + SubscriptionIdTemplateParameter + "/" + Providers + "/" +
                CertificateRegistrationResourceProviderName;

            public const string GlobalCertificateRegistrationResourceRootUri =
                CertificateRegistrationResourceProviderBasePath + GlobalCertificateRegistrationResourceRoot;

            public const string CertificateRegistrationProviderResourceRoot =
                CertificateRegistrationResourceProviderBasePath + "/providers/" +
                UriElements.Csm.CertificateRegistrationResourceProviderName;

            public const string CertificateOrdersResource = "certificateOrders";
            public const string CertificateOrderCertificatesResource = "certificates";

            public const string CertificateOrderNameTemplateParameterName = "certificateOrderName";

            public const string CertificateOrderNameTemplateParameter =
                "/{" + CertificateOrderNameTemplateParameterName + "}";

            public const string CertificateOrderReissueAction = "reissue";
            public const string CertificateOrderRenewAction = "renew";
            public const string CertificateOrderRetrieveCertificateActionsAction = "retrieveCertificateActions";
            public const string CertificateOrderRetrieveEmailHistoryAction = "retrieveEmailHistory";
            public const string CertificateOrderResendEmailAction = "resendEmail";
            public const string CertificateOrderVerifyDomainOwnership = "verifyDomainOwnership";
            public const string CertificateOrderResendRequestEmails = "resendRequestEmails";
            public const string CertificateOrderRetrieveSiteSeal = "retrieveSiteSeal";
            public const string ValidateCertificateRegistrationInformationAction = "validateCertificateRegistrationInformation";
            public static readonly string CertificateOrdersResourceType =
                UriElements.Csm.CertificateRegistrationResourceProviderName + "/" +
                UriElements.Csm.CertificateOrdersResource;

            public const string CertificateOrderIdTemplate =
                "/" + UriElements.Csm.CertificateRegistrationResourceRoot + "/" +
                UriElements.Csm.CertificateOrdersResource + UriElements.NameTemplateParameter;

            public const string OperationsApiRoute = "operations";

            public const string DomainOwnershipIdentifierTemplateParameterName = "domainOwnershipIdentifierName";

            public const string DomainOwnershipIdentifierTemplateParameter =
                "{" + DomainOwnershipIdentifierTemplateParameterName + "}";

            public const string DomainOwnershipIdentifiers = "domainOwnershipIdentifiers";

            public const string ReadOperation = "/Read";
            public const string WriteOperation = "/Write";
            public const string DeleteOperation = "/Delete";
            public const string ActionOperation = "/Action";
            public const string JoinActionOperation = "/Join/Action";
            public const string Notify = "/notify";

            public const string KeyVaultResourceProviderName = "Microsoft.KeyVault";
            public const string KeyVaultResourceType = "vaults";

            public const string VipMappings = "vipMappings";
            public const string LocalAddressMappings = "localAddressMappings";
            public const string VirtualIpTemplateParamater = "/{virtualIp}";

            public const string BuiltInPowerShellDnsProviders = "BuiltInPowerShellDnsProviders";
            public const string CopyToCustom = "/CopyToCustom";

            public const string IsWebAppCloneable = "/iscloneable";

            public const string FirstPartyApps = "firstPartyApps";
            public const string FirstPartyAppIdTemplate = "/{firstPartyAppId}";
            public const string Settings = "settings";
            public const string SettingNameTemplate = "/{settingName}";
            public const string FirstPartyAppSettingResourceFormat =
                "/subscriptions/{0}/resourceGroups/{1}/providers/" + UriElements.Csm.ResourceProviderName +
                "/serverFarms/{2}/firstPartyApps/{3}/settings/{4}";

            public const string Metrics = "metrics";
            //public const string EnvironmentNameParam = "{environmentName}";
            public const string NameParam = "{name}";

            public const string DeleteVnetOrSubnetsAction = "/deleteVirtualNetworkOrSubnets";
        }

        public class PricingTierPlatform
        {
            public const string windows = "windows";
            public const string xenon = "wcow";
            public const string linux = "linux";
            public const string lcow = "lcow";

            public static bool determinePlatformFlag(string plaftform, out bool isLinux, out bool isXenon)
            {
                isXenon = false;
                isLinux = false;

                if (string.Equals(plaftform, windows, StringComparison.CurrentCultureIgnoreCase))
                {
                    return true;
                }

                if (string.Equals(plaftform, xenon, StringComparison.CurrentCultureIgnoreCase))
                {
                    isXenon = true;
                    return true;
                }

                if (string.Equals(plaftform, linux, StringComparison.CurrentCultureIgnoreCase))
                {
                    isLinux = true;
                    return true;
                }

                if (string.Equals(plaftform, lcow, StringComparison.CurrentCultureIgnoreCase))
                {
                    isLinux = true;
                    isXenon = true;
                    return true;
                }

                return false;

            }

            public static string determinePlatform(bool isLinux, bool isXenon)
            {

                if (!isLinux && isXenon)
                {
                    return xenon;
                }

                if (isLinux && !isXenon)
                {
                    return linux;
                }

                if (isLinux && isXenon)
                {
                    return lcow;
                }

                return windows;

            }
        }

        public class AdminCsm
        {
            public const string SubscriptionLiteral = "subscriptions";
            public const string SubscriptionId = "subscriptionId";
            public const string SubscriptionIdVariable = "{" + SubscriptionId + "}";
            public const string SubscriptionIdPair = SubscriptionLiteral + "/" + SubscriptionIdVariable;

            public const string ResourceGroupLiteral = "resourceGroups";
            public const string ResourceGroupName = "resourceGroupName";
            public const string ResourceGroupNameVariable = "{" + ResourceGroupName + "}";
            public const string ResourceGroupPair = ResourceGroupLiteral + "/" + ResourceGroupNameVariable;

            public const string AdminNamespace = "Microsoft.Web.Admin";
            public const string AdminProviderPair = "providers/" + AdminNamespace;

            public const string LocationLiteral = "locations";
            public const string LocationVariable = "{location}";
            public const string LocationPair = LocationLiteral + "/" + LocationVariable;

            public const string AdminProviderLocationQuad = AdminProviderPair + "/" + LocationPair;

            public const string DefaultQuotaLiteral = "defaultQuota";
            public const string MetricsLiteral = "metrics";
            public const string SitesLiteral = "sites";
            public const string WorkerSanitizingAllDrivesLiteral = "workerSanitizingAllDrives";
            public const string ConfigLiteral = "config";
            public const string ValidLiteral = "valid";
            public const string WorkerSanitizingSuspendedLiteral = "workerSanitizingSuspended";
            public const string AuditLogsLiteral = "auditLogs";
            public const string OfflineLiteral = "offline";
            public const string OnlineLiteral = "online";
            public const string RebootLiteral = "reboot";
            public const string RemoveRunningSitesLiteral = "removeRunningSites";
            public const string RepairLiteral = "repair";
            public const string ValidRoleServerLiteral = "validateRoleServer";
            public const string OnLiteral = "on";
            public const string OffLiteral = "off";
            public const string UsageLiteral = "usage";
            public const string AdministrativeOperationsLiteral = "adminOps";
            public const string EncryptionKeysLiteral = "encryptionKeys";
            public const string ConnectionStringsLiteral = "connectionStrings";
            public const string CertificatesLiteral = "certificates";
            public const string SecretsLiteral = "secrets";

            public const string WorkerTiersLiteral = "workerTiers";
            public const string WorkerTierNameVariable = "{workerTierName}";
            public const string WorkerTierNamePair = WorkerTiersLiteral + "/" + WorkerTierNameVariable;

            public const string CustomProductsLiteral = "customProducts";
            public const string CustomProductIdVariable = "{customProductId}";
            public const string CustomProductIdPair = CustomProductsLiteral + "/" + CustomProductIdVariable;

            public const string SkusLiteral = "skus";
            public const string SkuNameVariable = "{skuName}";
            public const string SkuNamePair = SkusLiteral + "/" + SkuNameVariable;
            public const string PricingTiersLiteral = "pricingTiers";
            public const string PricngTierPlatformVariable = "{pricingTierPlatform}";
            public const string PrcingTierNameVariable = "{pricingTierName}";
            public const string PricingTierNamePair = PricingTiersLiteral + "/" + PricngTierPlatformVariable + "/" + PrcingTierNameVariable;

            public const string SystemsLiteral = "systems";
            public const string SystemNameVariable = "{systemName}";
            public const string SystemNamePair = SystemsLiteral + "/" + SystemNameVariable;

            public const string ServersLiteral = "servers";
            public const string ServerNameVariable = "{serverName}";
            public const string ServerNamePair = ServersLiteral + "/" + ServerNameVariable;

            public const string LogLiteral = "log";
            public const string LogDetails = "{startTime}/endTime/{endTime}";

            public const string RolesLiteral = "roles";
            public const string RoleIdVariable = "{roleId}";
            public const string RoleIdPair = RolesLiteral + "/" + RoleIdVariable;

            public const string WorkerSizeLiteral = "workerSize";
            public const string WorkerSizeIdVariable = "{workerSizeId}";
            public const string WorkerSizeIdPair = WorkerSizeLiteral + "/" + WorkerSizeIdVariable;

            public const string ComputeModeLiteral = "computeMode";
            public const string ComputeModeVariable = "{computeMode}";
            public const string ComputeModePair = ComputeModeLiteral + "/" + ComputeModeVariable;

            public const string CredentialsLiteral = "credentials";
            public const string CredentialNameVariable = "{credentialName}";
            public const string CredentialNamePair = CredentialsLiteral + "/" + CredentialNameVariable;

            public const string FeaturesLiteral = "features";
            public const string FeatureNameVariable = "{featureName}";
            public const string FeatureNamePair = FeaturesLiteral + "/" + FeatureNameVariable;

            public const string IpBlocklistsLiteral = "ipBlocklists";
            public const string IpBlockNameVariable = "{blockName}";

            public const string IpV4BlocklistsLiteral = IpBlocklistsLiteral + "V4";
            public const string IpV4BlockNamePair = IpV4BlocklistsLiteral + "/" + IpBlockNameVariable;

            public const string IpV6BlocklistsLiteral = IpBlocklistsLiteral + "V6";
            public const string IpV6BlockNamePair = IpV6BlocklistsLiteral + "/" + IpBlockNameVariable;

            public const string IpBlocklistsEnabledLiteral = IpBlocklistsLiteral + "enabled";

            public const string VipMappingsLiteral = "vipMappings";
            public const string VirtualIpVariable = "{virtualIp}";
            public const string VirtualIpPair = VipMappingsLiteral + "/" + VirtualIpVariable;

            public const string LocalAddressMappingsLiteral = "localAddressMappings";
            public const string LocalAddressVariable = "{localAddress}";
            public const string LocalAddressPair = LocalAddressMappingsLiteral + "/" + LocalAddressVariable;

            public const string BuiltInPowerShellDnsProvidersLiteral = "builtInPowerShellDnsProviders";
            public const string DnsProviderNameVariable = "{dnsProviderName}";
            public const string DnsProviderNamePair = BuiltInPowerShellDnsProvidersLiteral + "/" + DnsProviderNameVariable;

            public const string QuotasLiteral = "quotas";
            public const string QuotaNameVariable = "{quotaName}";
            public const string QuotaNamePair = QuotasLiteral + "/" + QuotaNameVariable;

            public const string ResourceTypeBase = AdminNamespace + "/" + LocationLiteral;

            public const string CompletedOperationLiteral = "completedOps";
            public const string OperationVariable = "{operationId}";
            public const string CompletedOperationPair = CompletedOperationLiteral + "/" + OperationVariable;
        }

        public class Metrics
        {
            public const string CpuTime = "CpuTime";
            public const string BytesReceived = "BytesReceived";
            public const string BytesSent = "BytesSent";
            public const string Request = "Requests";
            public const string TimeUnits = "Seconds";
            public const string BytesUnits = "Bytes";
            public const string BytesPerSecond = "BytesPerSecond";
            public const string MegaBytesUnits = "MegaBytes";
            public const string CountUnits = "Count";
            public const string ByteSeconds = "ByteSeconds";
            public const string Http101 = "Http101";
            public const string Http2xx = "Http2xx";
            public const string Http3xx = "Http3xx";
            public const string Http401 = "Http401";
            public const string Http403 = "Http403";
            public const string Http404 = "Http404";
            public const string Http406 = "Http406";
            public const string Http4xx = "Http4xx";
            public const string Http5xx = "Http5xx";
            public const string MemoryWorkingSet = "MemoryWorkingSet";
            public const string AverageMemoryWorkingSet = "AverageMemoryWorkingSet";
            public const string AverageResponseTime = "AverageResponseTime";
            public const string HttpResponseTime = "HttpResponseTime";
            public const string DiskQueueLength = "DiskQueueLength";
            public const string HttpQueueLength = "HttpQueueLength";
            public const string ActiveRequests = "ActiveRequests";
            public const string Connections = "AppConnections";
            public const string TcpSynSent = "TcpSynSent";
            public const string TcpSynReceived = "TcpSynReceived";
            public const string TcpEstablished = "TcpEstablished";
            public const string TcpFinWait1 = "TcpFinWait1";
            public const string TcpFinWait2 = "TcpFinWait2";
            public const string TcpClosing = "TcpClosing";
            public const string TcpCloseWait = "TcpCloseWait";
            public const string TcpLastAck = "TcpLastAck";
            public const string TcpTimeWait = "TcpTimeWait";
            public const string SocketInboundAll = "SocketInboundAll";
            public const string SocketOutboundAll = "SocketOutboundAll";
            public const string SocketOutboundEstablished = "SocketOutboundEstablished";
            public const string SocketOutboundTimeWait = "SocketOutboundTimeWait";
            public const string SocketLoopback = "SocketLoopback";
            public const string Handles = "Handles";
            public const string Threads = "Threads";
            public const string PrivateBytes = "PrivateBytes";
            public const string IOReadBytesPerSecond = "IoReadBytesPerSecond";
            public const string IOWriteBytesPerSecond = "IoWriteBytesPerSecond";
            public const string IOOtherBytesPerSecond = "IoOtherBytesPerSecond";
            public const string IOReadOperationsPerSecond = "IoReadOperationsPerSecond";
            public const string IOWriteOperationsPerSecond = "IoWriteOperationsPerSecond";
            public const string IOOtherOperationsPerSecond = "IoOtherOperationsPerSecond";
            public const string RequestsInApplicationQueue = "RequestsInApplicationQueue";
            public const string Gen0Collections = "Gen0Collections";
            public const string Gen1Collections = "Gen1Collections";
            public const string Gen2Collections = "Gen2Collections";
            public const string HealthCheckStatus = "HealthCheckStatus";
            public const string CurrentAssemblies = "CurrentAssemblies";
            public const string TotalAppDomains = "TotalAppDomains";
            public const string TotalAppDomainsUnloaded = "TotalAppDomainsUnloaded";
            public const string FileSystemUsage = "FileSystemUsage";
            public const string FunctionExecutionTime = "FunctionExecutionTime";
            public const string FunctionExecutionUnits = "FunctionExecutionUnits";
            public const string FunctionExecutionCount = "FunctionExecutionCount";
            public const string FunctionContainerSize = "FunctionContainerSize";
            public const string FunctionAppFilterPattern = "(?i:functionapp)";
            public const string LinuxAppFilterPattern = "(?i:linux)";
            public const string ExcludeFunctionAppFilterPatternName = "ExcludeMetricForFunctionFilterPattern";
            public const string WorkerPoolFilterPatternName = "WorkerPoolFilterPattern";
            public const string UseMdmForMetricsApi = "UseMdmForMetricsApi";
            public const string MdmMetricsRegions = "MdmMetricsRegions";
            public const string MdmMetricsStamps = "MdmMetricsStamps";
            public const string EnableMdmForLinux = "EnableMdmForLinux";
            public const string EnableMdmForHostingEnvironment = "EnableMdmForHostingEnvironment";
            public const string EnableMdmForResourcesOnHostingEnvironment = "EnableMdmForResourcesOnHostingEnvironment";
            public const string EnableMdmForDetailedMetrics = "EnableMdmForDetailedMetrics";
            public const string TotalFrontEnds = "TotalFrontEnds";
            public const string WorkersTotal = "WorkersTotal";
            public const string WorkersUsed = "WorkersUsed";
            public const string WorkersAvailable = "WorkersAvailable";
            public const string SmallAppServicePlanInstances = "SmallAppServicePlanInstances";
            public const string MediumAppServicePlanInstances = "MediumAppServicePlanInstances";
            public const string LargeAppServicePlanInstances = "LargeAppServicePlanInstances";
            public const string SiteHits = "SiteHits";
            public const string SiteErrors = "SiteErrors";
            public const string FunctionHits = "FunctionHits";
            public const string FunctionErrors = "FunctionErrors";

            public static TimeSpan TimeGrain1H = TimeSpan.FromHours(1);
            // order maters for result output in MetricDefinitions availability
            public static List<Tuple<string, string>> TimeGrainRetentions = new List<Tuple<string, string>>
            {
                // 1 minute grain - data retention 2 days
                new Tuple<string, string>("PT1M", "P2D"),

                // 1 hour grain - data retention 30 days
                new Tuple<string, string>("PT1H", "P30D"),

                // 1 day grain - data retention 90 days
                new Tuple<string, string>("P1D", "P90D"),
            };

            public const string CpuPercentage = "CpuPercentage";
            public const string MemoryPercentage = "MemoryPercentage";
            public const string PercentageUnits = "Percent";
            public const string Total = "Total";
            public const string Average = "Average";

            // values are used in DB sproc contract
            public enum TimeGrainValues
            {
                Minute = 0,
                Hour = 1,
                Day = 2,
            }
        }

        public class BasicPublishingCredentialsPolicies
        {
            public const string FtpResource = "ftp";
            public const string ScmResource = "scm";
        }
    }
}
