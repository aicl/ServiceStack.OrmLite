using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using ServiceStack.Logging;
using ServiceStack.Common;
using System.Linq.Expressions;

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
	
		public static void AddForeignKey<T,TForeign>(this IDbCommand dbCmdm,
		                                               Expression<Func<T,object>> field,
		                                               Expression<Func<TForeign,object>> foreignField,
		                                               OnFkOption onUpdate,
		                                               OnFkOption onDelete,
		                                               string foreignKeyName=null)
		{
			var sourceMD = ModelDefinition<T>.Definition;

			var fieldName= sourceMD.FieldDefinitions.First(f=>f.Name== FieldName(field)).FieldName;

			var referenceMD=ModelDefinition<TForeign>.Definition;

			var referenceFieldName= referenceMD.FieldDefinitions.First(f=>f.Name== FieldName(foreignField)).FieldName;

			string name =
				OrmLiteConfig.DialectProvider.GetQuotedName(foreignKeyName.IsNullOrEmpty()?
				                                            "fk_"+sourceMD.ModelName+"_"+ fieldName+"_"+referenceFieldName:
				                                            foreignKeyName);

			string command = string.Format("ALTER TABLE {0} ADD CONSTRAINT {1} FOREIGN KEY ({2}) REFERENCES {3} ({4}) ON UPDATE {5} ON DELETE {6};",
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(sourceMD.ModelName),
			                               name,
			                               OrmLiteConfig.DialectProvider.GetQuotedColumnName(fieldName),
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(referenceMD.ModelName),
			                               OrmLiteConfig.DialectProvider.GetQuotedColumnName(referenceFieldName),
			                               FkOptionToString(onUpdate),
			                               FkOptionToString(onDelete));

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

			var sourceMD = ModelDefinition<T>.Definition;
			var fieldName= sourceMD.FieldDefinitions.First(f=>f.Name== FieldName(field)).FieldName;

			string name =
				OrmLiteConfig.DialectProvider.GetQuotedName(indexName.IsNullOrEmpty()?
				                                            (unique?"uidx":"idx") +"_"+sourceMD.ModelName+"_"+fieldName:
				                                            indexName);

			string command = string.Format("CREATE{0}INDEX {1} ON {2}({3})",
			                               unique?" UNIQUE ": " ",
			                               name,
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(sourceMD.ModelName),
			                               OrmLiteConfig.DialectProvider.GetQuotedColumnName(fieldName)
			                               );
			dbCmdm.ExecuteSql(command);
		}

		public static void DropIndex<T>(this IDbCommand dbCmdm, string indexName)
		{
			string command = string.Format("ALTER TABLE {0} DROP INDEX  {1};",
			                               OrmLiteConfig.DialectProvider.GetQuotedTableName(ModelDefinition<T>.Definition.ModelName),
			                               OrmLiteConfig.DialectProvider.GetQuotedName(indexName));
			dbCmdm.ExecuteSql(command);
		}


		static string FieldName<T>(Expression<Func<T,object>> field){
			var lambda = (field as LambdaExpression);
			var operand = (lambda.Body as UnaryExpression).Operand ;
			return (operand as MemberExpression).Member.Name;
		}


		static string FkOptionToString(OnFkOption option){
			switch(option){
			case OnFkOption.Cascade: return "CASCADE";
			case OnFkOption.NoAction: return "NO ACTION"; 
			case OnFkOption.Restrict: return "RESTRICT"; 
			case OnFkOption.SetNull: return "SET NULL"; 
			default: return "RESTRICT";
			}
		}

	}
}
