using DataBaseTools.ViewModels;
using Renci.SshNet.Compression;
using System.Windows;
using System;

namespace DataBaseTools.Models
{
    public class OracleTable
    {
        public string? OWNER { get; set; }
        public string? TABLE_NAME { get; set; }
        public string? TABLESPACE_NAME { get; set; }
        public string? CLUSTER_NAME { get; set; }
        public string? IOT_NAME { get; set; }
        public string? STATUS { get; set; }
        public string? PCT_FREE { get; set; }
        public string? PCT_USED { get; set; }
        public string? INI_TRANS { get; set; }
        public string? MAX_TRANS { get; set; }
        public string? INITIAL_EXTENT { get; set; }
        public string? NEXT_EXTENT { get; set; }
        public string? MIN_EXTENTS { get; set; }
        public string? MAX_EXTENTS { get; set; }
        public string? PCT_INCREASE { get; set; }
        public string? FREELISTS { get; set; }
        public string? FREELIST_GROUPS { get; set; }
        public string? LOGGING { get; set; }
        public string? BACKED_UP { get; set; }
        public string? NUM_ROWS { get; set; }
        public string? BLOCKS { get; set; }
        public string? EMPTY_BLOCKS { get; set; }
        public string? AVG_SPACE { get; set; }
        public string? CHAIN_CNT { get; set; }
        public string? AVG_ROW_LEN { get; set; }
        public string? AVG_SPACE_FREELIST_BLOCKS { get; set; }
        public string? NUM_FREELIST_BLOCKS { get; set; }
        public string? DEGREE { get; set; }
        public string? INSTANCES { get; set; }
        public string? CACHE { get; set; }
        public string? TABLE_LOCK { get; set; }
        public string? SAMPLE_SIZE { get; set; }
        public string? LAST_ANALYZED { get; set; }
        public string? PARTITIONED { get; set; }
        public string? IOT_TYPE { get; set; }
        public string? TEMPORARY { get; set; }
        public string? SECONDARY { get; set; }
        public string? NESTED { get; set; }
        public string? BUFFER_POOL { get; set; }
        public string? FLASH_CACHE { get; set; }
        public string? CELL_FLASH_CACHE { get; set; }
        public string? ROW_MOVEMENT { get; set; }
        public string? GLOBAL_STATS { get; set; }
        public string? USER_STATS { get; set; }
        public string? DURATION { get; set; }
        public string? SKIP_CORRUPT { get; set; }
        public string? MONITORING { get; set; }
        public string? CLUSTER_OWNER { get; set; }
        public string? DEPENDENCIES { get; set; }
        public string? COMPRESSION { get; set; }
        public string? COMPRESS_FOR { get; set; }
        public string? DROPPED { get; set; }
        public string? READ_ONLY { get; set; }
        public string? SEGMENT_CREATED { get; set; }
        public string? RESULT_CACHE { get; set; }
        public string? CLUSTERING { get; set; }
        public string? ACTIVITY_TRACKING { get; set; }
        public string? DML_TIMESTAMP { get; set; }
        public string? HAS_IDENTITY { get; set; }
        public string? CONTAINER_DATA { get; set; }
        public string? INMEMORY { get; set; }
        public string? INMEMORY_PRIORITY { get; set; }
        public string? INMEMORY_DISTRIBUTE { get; set; }
        public string? INMEMORY_COMPRESSION { get; set; }
        public string? INMEMORY_DUPLICATE { get; set; }
        public string? DEFAULT_COLLATION { get; set; }
        public string? DUPLICATED { get; set; }
        public string? SHARDED { get; set; }
        public string? EXTERNALLY_SHARDED { get; set; }
        public string? EXTERNALLY_DUPLICATED { get; set; }
        public string? EXTERNAL { get; set; }
        public string? HYBRID { get; set; }
        public string? CELLMEMORY { get; set; }
        public string? CONTAINERS_DEFAULT { get; set; }
        public string? CONTAINER_MAP { get; set; }
        public string? EXTENDED_DATA_LINK { get; set; }
        public string? EXTENDED_DATA_LINK_MAP { get; set; }
        public string? INMEMORY_SERVICE { get; set; }
        public string? INMEMORY_SERVICE_NAME { get; set; }
        public string? CONTAINER_MAP_OBJECT { get; set; }
        public string? MEMOPTIMIZE_READ { get; set; }
        public string? MEMOPTIMIZE_WRITE { get; set; }
        public string? HAS_SENSITIVE_COLUMN { get; set; }
        public string? ADMIT_NULL { get; set; }
        public string? DATA_LINK_DML_ENABLED { get; set; }
        public string? LOGICAL_REPLICATION { get; set; }
    }
}
