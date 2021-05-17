using System;

namespace GetGenericMethods{
        public class GenericMethods{

        // method to generate the value inside the query
        public static Object CreateQuerieValue(string fieldName, string fieldOperator, string fieldValue){
            var querieValue = "";

            if(fieldName == "relationship_table" | fieldName == "relationship_value"){
                querieValue = "";
            }
            else{
                querieValue += $"{fieldName} {fieldOperator} {fieldValue} and ";
            } 
            return querieValue;
        }

        // method to get the relationship table
        public static string GetRelationshipValue(string fieldName, string fieldValue){
            var relationshipTable = "";
            
            if(fieldName == "relationship_table"){ 
                        relationshipTable = fieldValue;
                    }
            return relationshipTable.ToString();
        }

        // method to get the final expression with relational table
        public static void GetFinalSqlEsxpressionWithRelationShip(string sqlExpression, string relationshipQuery, string relationshipValue, string sqlSubqueryExpression){

            sqlSubqueryExpression = sqlSubqueryExpression.Remove(sqlSubqueryExpression.Length - 4, 4);
            
            var finalSqlEsxpressionWithRelationShip = $"{sqlExpression} {relationshipQuery}({relationshipValue} ({sqlSubqueryExpression})) ;";

                Console.WriteLine("Relationship Query Result:");
                Console.WriteLine(finalSqlEsxpressionWithRelationShip);
                Console.WriteLine("----------------");

        }
        
    }
}