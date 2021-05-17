using System;
using Newtonsoft.Json;
using System.IO;
using Newtonsoft.Json.Linq;
using GetGenericMethods;


public class CreateSQLQuery{   
    public static void Main(string[] args)
    {
        // create a json object
        JObject jsonObject = JObject.Parse(File.ReadAllText("data.json")); 

        // convert to string json
        string jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObject); 

        // create a dinamic object
        dynamic dobj = JsonConvert.DeserializeObject<dynamic>(jsonString); 

        foreach (var tables in dobj["tables"]){

            var relationshipTableName = "";
            var relationshipValue = "";
            var relationshipQuery = "";
            var sqlExpression = "";

            // add the first expression
            sqlExpression += $"Select * from {tables["table_name"]} where "; 
        
            foreach(var column in tables["columns"]){

                var fieldName = column["fieldName"].ToString();
                var fieldOperator = column["operator"].ToString();
                var fieldValue = column["fieldValue"].ToString();

                // method to get the querie value
                sqlExpression += GetGenericMethods.GenericMethods.CreateQuerieValue(fieldName,fieldOperator,fieldValue);

                // method to get the relationship table
                relationshipTableName = GetGenericMethods.GenericMethods.GetRelationshipValue(column["fieldName"].ToString(), column["fieldValue"].ToString());

                if(column["fieldName"].ToString() == "relationship_value"){ 
                    
                    // get the relationship value
                    relationshipValue = column["fieldValue"].ToString(); 
                    // create the relationship query
                    relationshipQuery = $"{column["fieldName"].ToString()} {column["operator"].ToString()} ";
                }
            }

            var finalSqlEsxpression = sqlExpression.Remove(sqlExpression.Length - 4, 4) + ";";

            Console.WriteLine("Query Result:");
            Console.WriteLine(finalSqlEsxpression); // final sql expression
            Console.WriteLine("----------------");

            bool verifyRelationshipVariable = string.IsNullOrEmpty(relationshipTableName);

            var sqlSubqueryExpression = "";

            // verify relationship 
            if(verifyRelationshipVariable is true){ 
                Console.WriteLine("Do not have relationship table");
                Console.WriteLine("----------------");
            }
            else{

                foreach(var table in dobj["tables"]){ 
                    
                    var subQueryColumns = table["columns"];

                    // get the relationship table
                    if (table["table_name"].ToString() == relationshipTableName){ 

                        sqlSubqueryExpression += $"Select * from {table["table_name"]} where ";

                        foreach(var subQueryColumn in subQueryColumns){

                            var fieldName = subQueryColumn["fieldName"].ToString();
                            var fieldOperator = subQueryColumn["operator"].ToString();
                            var fieldValue = subQueryColumn["fieldValue"].ToString();

                            // Get the subquery expression
                            sqlSubqueryExpression += GetGenericMethods.GenericMethods.CreateQuerieValue(fieldName,fieldOperator,fieldValue);
                        }
                    }
                }

                // create the final query with relationship
                 GetGenericMethods.GenericMethods.GetFinalSqlEsxpressionWithRelationShip(sqlExpression,  relationshipQuery,  relationshipValue,  sqlSubqueryExpression);
            }
        }
    }
}
