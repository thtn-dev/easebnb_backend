using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easebnb.Domain.Common.Constants;
 public static class DbTypeConstants
{
    public const string VARCHAR32 = "varchar(32)";
    public const string VARCHAR64 = "varchar(64)";
    public const string VARCHAR256 = "varchar(256)";
    public const string VARCHAR512 = "varchar(512)";

    public const string TEXT = "text";
    public const string LONGTEXT = "longtext";

    public const string INT = "int";
    public const string BIGINT = "bigint";
    public const string DECIMAL = "decimal(18,2)";
    public const string DATETIME = "datetime";
    public const string DATE = "date";
    public const string TIME = "time";
    public const string BOOLEAN = "bit";
    public const string JSON = "json";
    public const string JSONB = "jsonb";
    public const string UUID = "uuid";
    public const string ENUM = "enum";
    public const string BLOB = "blob";
}