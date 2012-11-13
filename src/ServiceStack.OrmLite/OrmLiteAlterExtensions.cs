using System;
using System.Data;
using System.Linq.Expressions;
using System.Linq;

namespace ServiceStack.OrmLite
{
	public enum  OnFkOption{
		Cascade,
		SetNull,
		NoAction,
		Restrict
	}

	public static class OrmLiteAlterExtensions
	{

		public static void AlterTable<T>(this IDbCommand dbCmdm, string command)
		{
			AlterTable(dbCmdm, typeof(T),command);
		}

		public static void AlterTable(this IDbCommand dbCmdm, Type modelType, string command)
		{
			string sql = string.Format("ALTER TABLE {0} {1};", 
			                           OrmLiteConfig.DialectProvider.GetQuotedTableName( modelType.GetModelDefinition()),
			                           command);
			dbCmdm.ExecuteSql(sql);
		}


		public static void AddColumn(this IDbCommand dbCmdm, Type modelType, FieldDefinition fieldDef){
			var command = OrmLiteConfig.DialectProvider.ToAddColumnStatement(modelType, fieldDef);
			dbCmdm.ExecuteSql(command);
		}

		public static void AddColumn<T>(this IDbCommand dbCmdm,
		                                Expression<Func<T,object>> field)
		{
			var modelDef = ModelDefinition<T>.Definition;
			var name = OrmLiteConfig.DialectProvider.FieldName(field);
			var fieldDef= modelDef.FieldDefinitions.First(f=>f.Name==name );
			dbCmdm.AddColumn(typeof(T), fieldDef);
		}


		public static void AlterColumn(this IDbCommand dbCmdm, Type modelType, FieldDefinition fieldDef){
			var command = OrmLiteConfig.DialectProvider.ToAddColumnStatement(modelType, fieldDef);
			dbCmdm.ExecuteSql(command);
		}

		public static void AlterColumn<T>(this IDbCommand dbCmdm,
		                                  Expression<Func<T,object>> field)
		{
			var modelDef = ModelDefinition<T>.Definition;
			var name = OrmLiteConfig.DialectProvider.FieldName(field);
			var fieldDef= modelDef.FieldDefinitions.First(f=>f.Name== name);
			dbCmdm.AlterColumn(typeof(T), fieldDef);

		}

		public static void ChangeColumnName(this IDbCommand dbCmdm,Type modelType,
		                                       FieldDefinition fieldDef,
		                                       string oldColumnName)
		{
			var command = OrmLiteConfig.DialectProvider.ToChangeColumnNameStatement(modelType, fieldDef, oldColumnName);
			dbCmdm.ExecuteSql(command);

		}

		public static void ChangeColumnName<T>(this IDbCommand dbCmdm,
		                                       Expression<Func<T,object>> field,
		                                       string oldColumnName)
		{
			var modelDef = ModelDefinition<T>.Definition;
			var name = OrmLiteConfig.DialectProvider.FieldName(field);
			var fieldDef= modelDef.FieldDefinitions.First(f=>f.Name== name);
			dbCmdm.ChangeColumnName(typeof(T), fieldDef, oldColumnName);

		}

		public static void DropColumn(this IDbCommand dbCmdm,Type modelType, string columnName)
		{
			string command = string.Format("ALTER TABLE {0} DROP  {1};",
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(modelType.GetModelName()),
			                               OrmLiteConfig.DialectProvider.GetQuotedName(columnName));

			dbCmdm.ExecuteSql(command);
		}

		public static void DropColumn<T>(this IDbCommand dbCmdm,string columnName)
		{
			dbCmdm.DropColumn(typeof(T), columnName);
		}

	
		public static void AddForeignKey<T,TForeign>(this IDbCommand dbCmdm,
		                                               Expression<Func<T,object>> field,
		                                               Expression<Func<TForeign,object>> foreignField,
		                                               OnFkOption onUpdate,
		                                               OnFkOption onDelete,
		                                               string foreignKeyName=null)
		{

			string command = OrmLiteConfig.DialectProvider.ToAddForeignKeyStatement(field,
			                                                                        foreignField,
			                                                                        onUpdate,
			                                                                        onDelete,
			                                                                        foreignKeyName);
			dbCmdm.ExecuteSql(command);

		}

		public static void DropForeignKey<T>(this IDbCommand dbCmdm,string foreignKeyName)
		{
			string command = string.Format("ALTER TABLE {0} DROP FOREIGN KEY {1};",
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(ModelDefinition<T>.Definition.ModelName),
			                               OrmLiteConfig.DialectProvider.GetQuotedName(foreignKeyName));

			dbCmdm.ExecuteSql(command);
		}

		public static void CreateIndex<T>(this IDbCommand dbCmdm,Expression<Func<T,object>> field,
		                                     string indexName=null, bool unique=false)
		{

			var command = OrmLiteConfig.DialectProvider.ToCreateIndexStatement(field, indexName, unique);
			dbCmdm.ExecuteSql(command);
		}

		public static void DropIndex<T>(this IDbCommand dbCmdm, string indexName)
		{
			string command = string.Format("ALTER TABLE {0} DROP INDEX  {1};",
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(ModelDefinition<T>.Definition.ModelName),
			                               OrmLiteConfig.DialectProvider.GetQuotedName(indexName));
			dbCmdm.ExecuteSql(command);
		}


	}
}
