/*
 * Instruktioner för att uppdatera databasen.
 * 1. Gör aktuella ändringar i Entities.xml.
 * 2. Generera.
 * 3. Kör EasyBase och "Visa XML" och kopiera XML-datat.
 * 4. Klistra in XML-datat i funktionen GetDatabaseXmlDefinition(int) i den här filen.
 * 5. Uppdatera versionsnumret i Settings.
 * 6. Uppdatera funktionen InitDatabaseOnCreate i filen DatabaseConversions.cs med en ny defaultdata (utan att använda några genererade objekt).
 * 7. Uppdatera funktionen ConvertDatabase i filen DatabaseConversions.cs med ny uppdateringsdata (utan att använda några genererade objekt).
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EasyBase.Classes;

namespace EasyBase.DataLayer
{
	public static class DatabaseDefinition
	{
        /// <summary>
        /// Gets the current database version.
        /// </summary>
        public static int DatabaseVersion
        {
            get { return 9; }
        }

        /// <summary>
        /// Gets the database XML schema definition for a specified database version.
        /// </summary>
        /// <param name="databaseVersion">The database version.</param>
        /// <remarks>
        /// The database XML schema definition is hard coded in this function and for each new database version the database XML schema definition must be entered.
        /// </remarks>
        /// <returns>The database XML schema definition as a string.</returns>
		public static string GetDatabaseXmlDefinition(int databaseVersion)
		{
			switch (databaseVersion) {
                case 1:
                    return @"
<Database Version='1'>
  <Tables>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Description' Type='varchar(1024)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Description' Type='varchar(1024)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='SourceAccountTransactionNo' Type='int' TargetTable='AccountTransactions' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='TransactionTime' Type='datetime' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
        <ViewField Name='AccountBalance' Type='money' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='SourceAccountTransactionNo' Type='int' TargetTable='AccountTransactions' TargetField='No' />
        <Field Name='TransactionTime' Type='datetime' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBoxLog'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(1024)' />
        <Field Name='Time' Type='datetime' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 2:
                    return @"
<Database Version='2'>
  <Tables>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Description' Type='varchar(1024)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Description' Type='varchar(1024)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='SourceAccountTransactionNo' Type='int' TargetTable='AccountTransactions' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='TransactionTime' Type='datetime' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
        <ViewField Name='AccountBalance' Type='money' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='SourceAccountTransactionNo' Type='int' TargetTable='AccountTransactions' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
        <ViewField Name='AccountTransactionTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='CashBoxLog'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(1024)' />
        <Field Name='Time' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 3:
                    return @"
<Database Version='3'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 4:
			        return @"
<Database Version='4'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Value' Type='money' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 5:
			        return @"
<Database Version='5'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Value' Type='money' />
        <Field Name='MoneyValue' Type='money' />
        <Field Name='RelativeValue' Type='decimal(18,9)' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 6:
			        return @"
<Database Version='6'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Value' Type='money' />
        <Field Name='MoneyValue' Type='money' />
        <Field Name='RelativeValue' Type='decimal(18,9)' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 7:
			        return @"
<Database Version='7'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='MoneyValue' Type='money' />
        <Field Name='RelativeValue' Type='decimal(18,9)' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='CashBoxSettings'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='AccountingYear' Type='int' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 8:
			        return @"
<Database Version='8'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
        <Field Name='AccountingDate' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='MoneyValue' Type='money' />
        <Field Name='RelativeValue' Type='decimal(18,9)' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountingDate' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountingDate' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='CashBoxSettings'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='AccountingYear' Type='int' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
                case 9:
			        return @"
<Database Version='9'>
  <Tables>
    <Table Name='Verifications'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Year' Type='int' UniqueKeyGroup='1' />
        <Field Name='SerialNo' Type='int' UniqueKeyGroup='1' />
        <Field Name='Date' Type='smalldatetime' />
        <Field Name='AccountingDate' Type='smalldatetime' />
      </Fields>
    </Table>
    <Table Name='Users'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='Email' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='Password' Type='varchar(128)' />
        <Field Name='CreatedTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='Categories'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='ParentCategoryNo' Type='int' TargetTable='Categories' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <Field Name='ShowInDiagram' Type='bit' />
      </Fields>
    </Table>
    <Table Name='Accounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='Name' Type='varchar(128)' />
        <Field Name='BalanceBroughtForwardAmount' Type='money' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <Field Name='ShowInDiagram' Type='bit' />
        <ViewField Name='Balance' Type='money' />
      </Fields>
    </Table>
    <Table Name='TagAccounts' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <Field Name='Name' Type='varchar(128)' UniqueKeyGroup='1' />
        <Field Name='IsArchived' Type='bit' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTags' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <Field Name='Type' Type='int' />
        <Field Name='MoneyValue' Type='money' />
        <Field Name='RelativeValue' Type='decimal(18,9)' />
        <ViewField Name='TagAccountName' Type='varchar(128)' />
        <ViewField Name='Amount' Type='money' />
      </Fields>
    </Table>
    <Table Name='AccountTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountingDate' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
        <ViewField Name='UserName' Type='varchar(128)' />
        <ViewField Name='ShowInDiagram' Type='bit' />
      </Fields>
    </Table>
    <Table Name='TagAccountSnapshots' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='TagAccountNo' Type='int' TargetTable='TagAccounts' TargetField='No' />
        <Field Name='BalanceAmount' Type='money' />
        <Field Name='Reason' Type='int' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='AccountName' Type='varchar(128)' />
      </Fields>
    </Table>
    <Table Name='CashBookTransactions' IsView='true'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' />
        <ForeignKeyField Name='CategoryNo' Type='int' TargetTable='Categories' TargetField='No' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' />
        <Field Name='Amount' Type='money' />
        <Field Name='Note' Type='varchar(1024)' />
        <Field Name='CreatedTime' Type='datetime' />
        <ViewField Name='VerificationSerialNo' Type='int' />
        <ViewField Name='TransactionTime' Type='datetime' />
        <ViewField Name='AccountingDate' Type='datetime' />
        <ViewField Name='CategoryName' Type='varchar(128)' />
        <ViewField Name='ShowInDiagram' Type='bit' />
      </Fields>
    </Table>
    <Table Name='LogItems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' IsIdentity='true' />
        <ForeignKeyField Name='UserNo' Type='int' TargetTable='Users' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='VerificationNo' Type='int' TargetTable='Verifications' TargetField='No' AllowNull='true' />
        <ForeignKeyField Name='AccountNo' Type='int' TargetTable='Accounts' TargetField='No' AllowNull='true' />
        <Field Name='Type' Type='int' />
        <Field Name='Description' Type='nvarchar(MAX)' />
        <Field Name='LogTime' Type='datetime' />
      </Fields>
    </Table>
    <Table Name='CashBoxSettings'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='AccountingYear' Type='int' />
      </Fields>
    </Table>
    <Table Name='EasyBaseSystems'>
      <Fields>
        <PrimaryKeyField Name='No' Type='int' />
        <Field Name='DatabaseVersion' Type='int' />
        <Field Name='UpdatingToVersion' Type='int' />
      </Fields>
    </Table>
  </Tables>
</Database>";
				default:
					throw new DatabaseSchemaException("Function GetDatabaseXmlDefinition does not contain the XML-definition for database version " + databaseVersion + ".");
			}
		}

	    /// <summary>
	    /// Gets a table's view definition in SQL-code for a given database version.
	    /// </summary>
	    /// <param name="tableName">Table name.</param>
	    /// <param name="databaseVersion">Database version.</param>
	    /// <param name="createOrderIndex">Returns an integer for the order in which to create views with the same database version.</param>
	    /// <remarks>
	    /// If an existing view changes, the old view will be dropped and replaced with the new.
	    /// </remarks>
	    /// <returns>A table's view definition in SQL-code. An empty string if no view definition could be found.</returns>
	    public static string GetViewSql(string tableName, int databaseVersion, out int createOrderIndex)
        {
            if (databaseVersion == 1) {
                if (tableName == "AccountTransactions")
                {
                    createOrderIndex = 0;
                    return
                        @"select
					        at1.*,
					        Users.Name as UserName,
					        Accounts.Name as AccountName,
                            (
                                select sum(Amount)
                                from AccountTransactions at2
                                where
                                    at2.AccountNo = at1.AccountNo and
                                    (
                                        (
                                            at2.No <= at1.No and
                                            at2.TransactionTime <= at1.TransactionTime
                                        ) or
                                        at2.TransactionTime < at1.TransactionTime
                                    )
                            ) as AccountBalance
				        from
					        AccountTransactions at1
					        join Accounts on at1.AccountNo = Accounts.No
					        join Users on at1.UserNo = Users.No";
                }
                else if (tableName == "CashBookTransactions") {
                    createOrderIndex = 1;
                    return
                        @"select
					        CashBookTransactions.*,
					        Users.Name as UserName,
                            Categories.Name as CategoryName
				        from
					        CashBookTransactions
					        join Users on CashBookTransactions.UserNo = Users.No
					        join Categories on CashBookTransactions.CategoryNo = Categories.No";
                }
            }
            else if (databaseVersion == 2) {
                createOrderIndex = 0;
                if (tableName == "Accounts")
                {
                    return
                        @"select
                            Accounts.*,
                            (
                                select isnull(sum(Amount),0)
                                from AccountTransactions
                                where AccountNo = Accounts.No
                            ) as Balance
                        from
                            Accounts";
                }
                else if (tableName == "CashBookTransactions") {
                    return @"
                        select
    					    CashBookTransactions.*,
                            AccountTransactions.TransactionTime as AccountTransactionTime,
	    				    Users.Name as UserName,
                            Categories.Name as CategoryName
			    	    from
				    	    CashBookTransactions
                            join AccountTransactions on CashBookTransactions.SourceAccountTransactionNo = AccountTransactions.No
					        join Users on CashBookTransactions.UserNo = Users.No
					        join Categories on CashBookTransactions.CategoryNo = Categories.No";
                }
            }
            else if (databaseVersion == 3)
            {
                createOrderIndex = 0;
                if (tableName == "AccountTransactions") {
                    return @"
                        select
					        at1.*,
                            v1.SerialNo as VerificationSerialNo,
                            v1.Date as TransactionTime,
					        Users.Name as UserName,
					        Accounts.Name as AccountName
				        from
					        AccountTransactions at1
                            join Verifications v1 on at1.VerificationNo = v1.No
					        join Accounts on at1.AccountNo = Accounts.No
					        join Users on at1.UserNo = Users.No";
                }
                else if (tableName == "CashBookTransactions")
                {
                    createOrderIndex = 1;
                    return @"
                        select
    					    CashBookTransactions.*,
                            Verifications.SerialNo as VerificationSerialNo,
                            Verifications.Date as TransactionTime,
	    				    Users.Name as UserName,
                            Categories.Name as CategoryName
			    	    from
				    	    CashBookTransactions
                            join Verifications on CashBookTransactions.VerificationNo = Verifications.No
					        join Users on CashBookTransactions.UserNo = Users.No
					        join Categories on CashBookTransactions.CategoryNo = Categories.No";
                }
            }
            else if (databaseVersion == 4)
            {
                if (tableName == "AccountTags")
                {
                    createOrderIndex = 0;
                    return string.Format(@"
                        select
	                        AccountTags.*,
                            TagAccounts.Name as TagAccountName,
	                        (
		                        case 
			                        when AccountTags.[Type] = {0} then
				                        [Value]
			                        when AccountTags.[Type] = {1} then
				                        (
					                        (
						                        AccountsView.Balance + AccountsView.BalanceBroughtForwardAmount
						                        -
						                        (
							                        select isnull(sum(at.Value),0)
							                        from AccountTags at
							                        where
								                        at.AccountNo = AccountsView.No and
								                        at.Type = {0}
						                        )
					                        )
					                        *
					                        AccountTags.Value
				                        )
		                        end
	                        ) as Amount
                        from AccountTags
                        join TagAccounts on AccountTags.TagAccountNo = TagAccounts.No
                        join AccountsView on AccountTags.AccountNo = AccountsView.No",
                                                                                     (int)AccountTagType.ExactAmount, 
                                                                                     (int)AccountTagType.PercentOfRest);
                }
                else if (tableName == "TagAccounts")
                {
                    createOrderIndex = 1;
                    return @"
                        select
                            TagAccounts.*,
                            (
                                select sum(Amount)
                                    from AccountTagsView
                                    where
                                        TagAccountNo = TagAccounts.No
                            ) as Amount
                        from
                            TagAccounts";
                }
                else if (tableName == "TagAccountSnapshots")
                {
                    createOrderIndex = 2;
                    return @"
                        select
                            TagAccountSnapshots.*,
                            TagAccounts.Name
                        from
                            TagAccountSnapshots
                            join TagAccounts on TagAccountSnapshots.TagAccountNo = TagAccounts.No";
                }
            }
            else if (databaseVersion == 5)
            {
                if (tableName == "AccountTags")
                {
                    createOrderIndex = 0;
                    return string.Format(@"
                        select
	                        AccountTags.*,
                            TagAccounts.Name as TagAccountName,
	                        (
		                        case 
			                        when AccountTags.[Type] = {0} then
				                        [MoneyValue]
			                        when AccountTags.[Type] = {1} then
				                        (
					                        (
						                        AccountsView.Balance + AccountsView.BalanceBroughtForwardAmount
						                        -
						                        (
							                        select isnull(sum(at.MoneyValue),0)
							                        from AccountTags at
							                        where
								                        at.AccountNo = AccountsView.No and
								                        at.Type = {0}
						                        )
					                        )
					                        *
					                        AccountTags.RelativeValue
				                        )
		                        end
	                        ) as Amount
                        from AccountTags
                        join TagAccounts on AccountTags.TagAccountNo = TagAccounts.No
                        join AccountsView on AccountTags.AccountNo = AccountsView.No",
                        (int) AccountTagType.ExactAmount,
                        (int) AccountTagType.PercentOfRest);
                }
                else if (tableName == "TagAccounts")
                {
                    createOrderIndex = 1;
                    return @"
                        select
                            TagAccounts.*,
                            (
                                select sum(Amount)
                                    from AccountTagsView
                                    where
                                        TagAccountNo = TagAccounts.No
                            ) as Amount
                        from
                            TagAccounts";
                }
            }
            else if (databaseVersion == 8)
            {
                createOrderIndex = 0;
                if (tableName == "AccountTransactions") {
                    return @"
                        select
					        at1.*,
                            v1.SerialNo as VerificationSerialNo,
                            v1.Date as TransactionTime,
                            v1.AccountingDate as AccountingDate,
					        Users.Name as UserName,
					        Accounts.Name as AccountName
				        from
					        AccountTransactions at1
                            join Verifications v1 on at1.VerificationNo = v1.No
					        join Accounts on at1.AccountNo = Accounts.No
					        join Users on at1.UserNo = Users.No";
                }
                else if (tableName == "CashBookTransactions")
                {
                    createOrderIndex = 1;
                    return @"
                        select
    					    CashBookTransactions.*,
                            Verifications.SerialNo as VerificationSerialNo,
                            Verifications.Date as TransactionTime,
                            Verifications.AccountingDate as AccountingDate,
	    				    Users.Name as UserName,
                            Categories.Name as CategoryName
			    	    from
				    	    CashBookTransactions
                            join Verifications on CashBookTransactions.VerificationNo = Verifications.No
					        join Users on CashBookTransactions.UserNo = Users.No
					        join Categories on CashBookTransactions.CategoryNo = Categories.No";
                }
            }
            else if (databaseVersion == 9) {
                createOrderIndex = 0;
                if (tableName == "AccountTransactions") {
                    return @"
                        select
					        at1.*,
                            v1.SerialNo as VerificationSerialNo,
                            v1.Date as TransactionTime,
                            v1.AccountingDate as AccountingDate,
					        Users.Name as UserName,
					        Accounts.Name as AccountName,
                            Accounts.ShowInDiagram as ShowInDiagram
				        from
					        AccountTransactions at1
                            join Verifications v1 on at1.VerificationNo = v1.No
					        join Accounts on at1.AccountNo = Accounts.No
					        join Users on at1.UserNo = Users.No";
                }
                else if (tableName == "CashBookTransactions") {
                    createOrderIndex = 1;
                    return @"
                        select
    					    CashBookTransactions.*,
                            Verifications.SerialNo as VerificationSerialNo,
                            Verifications.Date as TransactionTime,
                            Verifications.AccountingDate as AccountingDate,
	    				    Users.Name as UserName,
                            Categories.Name as CategoryName,
                            Categories.ShowInDiagram as ShowInDiagram
			    	    from
				    	    CashBookTransactions
                            join Verifications on CashBookTransactions.VerificationNo = Verifications.No
					        join Users on CashBookTransactions.UserNo = Users.No
					        join Categories on CashBookTransactions.CategoryNo = Categories.No";
                }
            }

	        createOrderIndex = 0;
            return "";
        }
    }
}

