CREATE DATABASE  IF NOT EXISTS `invoicemanager1` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `invoicemanager1`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: invoicemanager1
-- ------------------------------------------------------
-- Server version	5.7.19-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `addresses`
--

DROP TABLE IF EXISTS `addresses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `addresses` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Address1` varchar(100) NOT NULL,
  `Address2` varchar(100) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `State` varchar(45) DEFAULT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `PostalCode` varchar(10) DEFAULT NULL,
  `ContactPerson` varchar(100) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Fax` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `addresses`
--

LOCK TABLES `addresses` WRITE;
/*!40000 ALTER TABLE `addresses` DISABLE KEYS */;
/*!40000 ALTER TABLE `addresses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `billinginformation`
--

DROP TABLE IF EXISTS `billinginformation`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `billinginformation` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Name` varchar(100) NOT NULL,
  `BillingGroupId` varchar(45) DEFAULT NULL,
  `PaymentType` varchar(10) NOT NULL DEFAULT 'CASH',
  `PercentDiscount` float DEFAULT NULL,
  `Note` varchar(255) DEFAULT NULL,
  `HSTNumber` varchar(45) DEFAULT NULL,
  `PSTExempt` bit(1) DEFAULT b'0',
  `Status` varchar(10) NOT NULL DEFAULT 'ACT',
  `Address1` varchar(100) DEFAULT NULL,
  `Address2` varchar(100) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `State` varchar(45) DEFAULT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `PostalCode` varchar(10) DEFAULT NULL,
  `ContactPerson` varchar(100) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Fax` varchar(45) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `billinginformation`
--

LOCK TABLES `billinginformation` WRITE;
/*!40000 ALTER TABLE `billinginformation` DISABLE KEYS */;
INSERT INTO `billinginformation` VALUES (1,'Petro Can test','232323','CHARG',25,NULL,NULL,'\0','ACT','156 Toronto Rd',NULL,'Port Hope','Ontario',NULL,'L1A 3S5',NULL,NULL,NULL,'2018-07-21 17:49:01','2018-12-03 09:40:57'),(2,'Petro Can',NULL,'CHARG',25,NULL,NULL,'','ACT','Devision St.',NULL,'Cobourg','Ontario',NULL,'',NULL,NULL,NULL,'2018-07-29 10:09:47','2018-12-03 09:41:41'),(3,'Kingston Rd Esso','600001','CASH',30,NULL,NULL,'\0','ACT','4660 Kingston Rd. ',NULL,'Scarborough','ON',NULL,NULL,NULL,NULL,NULL,'2018-10-28 09:52:10','2018-10-28 09:52:10'),(4,'Macs','345','CHARG',25,NULL,NULL,'\0','ACT','asdf',NULL,'asdf','On',NULL,'M3K 3l4','234','23423',NULL,'2018-12-03 09:25:32','2018-12-03 09:25:32');
/*!40000 ALTER TABLE `billinginformation` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `deliveryroutes`
--

DROP TABLE IF EXISTS `deliveryroutes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `deliveryroutes` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RouteNumber` varchar(10) NOT NULL,
  `Description` varchar(45) DEFAULT NULL,
  `DeliveryDate` date DEFAULT NULL,
  `DeliveryDay` int(11) DEFAULT '0',
  `ProductionDay` int(11) DEFAULT NULL,
  `RecordCreateDate` datetime DEFAULT NULL,
  `RecordUpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `deliveryroutes`
--

LOCK TABLES `deliveryroutes` WRITE;
/*!40000 ALTER TABLE `deliveryroutes` DISABLE KEYS */;
INSERT INTO `deliveryroutes` VALUES (1,'100','North Run Test',NULL,1,6,'2018-07-21 17:22:43','2018-07-29 09:18:05'),(2,'300','Kingston',NULL,2,2,'2018-07-21 17:23:32','2018-07-29 10:35:07'),(3,'600','600',NULL,3,3,'2018-10-28 09:53:26','2018-10-28 09:53:26');
/*!40000 ALTER TABLE `deliveryroutes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orderitems`
--

DROP TABLE IF EXISTS `orderitems`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orderitems` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `OrderId` int(11) NOT NULL,
  `ProductId` int(11) DEFAULT NULL,
  `ItemsOrdered` int(11) DEFAULT NULL,
  `ItemsReturned` int(11) DEFAULT NULL,
  `ItemsSold` int(11) DEFAULT NULL,
  `ItemPrice` decimal(10,4) DEFAULT '0.0000',
  `CreateDate` datetime DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  `RecordStatus` varchar(10) DEFAULT 'ACT',
  `DiscountPrice` decimal(10,4) DEFAULT '0.0000',
  `SubTotal` decimal(10,4) DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_orderitems_orderid_orders_id_idx` (`OrderId`),
  KEY `FK_orderitems_productid_products_id_idx` (`ProductId`),
  CONSTRAINT `FK_orderitems_orderid_orders_id` FOREIGN KEY (`OrderId`) REFERENCES `orders` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_orderitems_productid_products_id` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=77 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orderitems`
--

LOCK TABLES `orderitems` WRITE;
/*!40000 ALTER TABLE `orderitems` DISABLE KEYS */;
INSERT INTO `orderitems` VALUES (13,5,1,4,0,4,10.9900,'2018-08-06 12:37:02','2018-09-28 14:15:01','ACT',8.2400,32.9600),(14,5,2,1,0,1,10.9900,'2018-08-06 12:37:02','2018-09-28 14:15:01','ACT',8.2400,8.2400),(15,5,5,3,0,3,10.9900,'2018-08-06 12:37:02','2018-09-28 14:15:01','ACT',8.2400,24.7200),(16,5,5,3,0,3,10.9900,'2018-08-06 12:37:02','2018-08-06 12:39:12','DEL',8.2400,24.7200),(17,6,1,4,0,4,10.9900,'2018-10-13 17:44:55','2018-10-13 17:44:55','ACT',8.2400,32.9600),(18,6,2,1,0,1,10.9900,'2018-10-13 17:44:55','2018-10-13 17:44:55','ACT',8.2400,8.2400),(19,6,5,3,0,3,10.9900,'2018-10-13 17:44:55','2018-10-13 17:44:55','ACT',8.2400,24.7200),(20,6,5,3,0,3,10.9900,'2018-10-13 17:44:55','2018-10-13 17:44:55','ACT',8.2400,24.7200),(21,9,2,1,0,1,10.9900,'2018-10-23 13:43:35','2018-10-23 13:43:35','ACT',8.2400,8.2400),(22,10,2,1,0,1,10.9900,'2018-10-23 13:43:51','2018-10-23 13:43:51','ACT',8.2400,8.2400),(23,11,2,1,0,1,10.9900,'2018-10-23 13:43:53','2018-10-23 13:43:53','ACT',8.2400,8.2400),(24,12,2,1,0,1,10.9900,'2018-10-23 13:43:54','2018-10-23 13:43:54','ACT',8.2400,8.2400),(25,13,2,1,0,1,10.9900,'2018-10-23 14:24:14','2018-10-23 14:24:14','ACT',8.2400,8.2400),(26,19,1,4,0,4,10.9900,'2018-10-28 10:03:48','2018-10-28 10:03:48','ACT',8.2400,32.9600),(27,19,2,1,0,1,10.9900,'2018-10-28 10:03:48','2018-10-28 10:03:48','ACT',8.2400,8.2400),(28,19,5,3,0,3,10.9900,'2018-10-28 10:03:48','2018-10-28 10:03:48','ACT',8.2400,24.7200),(29,19,5,3,0,3,10.9900,'2018-10-28 10:03:48','2018-10-28 10:03:48','ACT',8.2400,24.7200),(30,19,1,4,0,4,10.9900,'2018-10-28 10:03:48','2018-10-28 10:03:48','ACT',8.2400,32.9600),(31,20,1,4,0,4,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',8.2400,32.9600),(32,20,1,4,0,4,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',8.2400,32.9600),(33,20,1,4,0,4,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',8.2400,32.9600),(34,20,1,4,0,4,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',8.2400,32.9600),(35,21,1,4,0,4,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',7.6900,30.7600),(36,21,2,1,0,1,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',7.6900,7.6900),(37,21,5,3,0,3,10.9900,'2018-10-28 10:03:49','2018-10-28 10:03:49','ACT',7.6900,23.0700),(38,22,1,4,0,0,10.9900,'2018-10-28 10:53:32','2018-10-28 10:53:32','ACT',0.0000,0.0000),(39,22,2,1,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(40,22,5,3,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(41,22,5,3,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(42,22,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(43,23,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(44,23,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(45,23,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(46,23,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(47,24,1,4,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(48,24,2,1,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(49,24,5,3,0,0,10.9900,'2018-10-28 10:53:33','2018-10-28 10:53:33','ACT',0.0000,0.0000),(50,25,1,4,0,4,10.9900,'2018-10-28 11:00:23','2018-10-28 11:00:23','ACT',7.6900,30.7600),(51,25,2,3,0,3,10.9900,'2018-10-28 11:00:23','2018-10-28 11:00:23','ACT',7.6900,23.0700),(52,25,3,2,0,2,15.9900,'2018-10-28 11:00:23','2018-10-28 11:00:23','ACT',11.1900,22.3800),(53,26,2,1,0,0,10.9900,'2018-11-03 21:46:09','2018-11-03 21:46:09','ACT',0.0000,0.0000),(54,26,5,3,0,0,10.9900,'2018-11-03 21:46:09','2018-11-03 21:46:09','ACT',0.0000,0.0000),(55,26,5,3,0,0,10.9900,'2018-11-03 21:46:09','2018-11-03 21:46:09','ACT',0.0000,0.0000),(56,27,2,1,0,0,10.9900,'2018-11-03 21:46:09','2018-11-03 21:46:09','ACT',0.0000,0.0000),(57,27,5,3,0,0,10.9900,'2018-11-03 21:46:09','2018-11-03 21:46:09','ACT',0.0000,0.0000),(58,28,1,4,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(59,28,2,1,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(60,28,5,3,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(61,28,5,3,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(62,29,1,4,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(63,30,1,4,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(64,30,2,1,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(65,30,5,3,0,0,10.9900,'2018-12-03 08:36:49','2018-12-03 08:36:49','ACT',0.0000,0.0000),(66,31,1,4,0,4,10.9900,'2018-12-03 08:49:38','2018-12-07 21:52:53','ACT',0.0000,32.9600),(67,31,2,1,0,1,10.9900,'2018-12-03 08:49:38','2018-12-07 21:52:53','ACT',0.0000,8.2400),(68,31,5,3,0,3,10.9900,'2018-12-03 08:49:38','2018-12-07 21:52:53','ACT',0.0000,24.7200),(69,31,5,3,0,3,10.9900,'2018-12-03 08:49:38','2018-12-07 21:52:53','ACT',0.0000,24.7200),(70,32,1,4,0,0,10.9900,'2018-12-03 08:49:38','2018-12-03 08:49:38','ACT',0.0000,0.0000),(74,34,1,4,0,0,10.9900,'2018-12-07 21:56:03','2018-12-07 21:56:03','ACT',7.6900,0.0000),(75,34,2,1,0,0,10.9900,'2018-12-07 21:56:03','2018-12-07 21:56:03','ACT',7.6900,0.0000),(76,34,5,3,0,0,10.9900,'2018-12-07 21:56:03','2018-12-07 21:56:03','ACT',7.6900,0.0000);
/*!40000 ALTER TABLE `orderitems` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `orders`
--

DROP TABLE IF EXISTS `orders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `orders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `InvoiceNumber` int(11) DEFAULT '0',
  `StoreId` int(11) NOT NULL,
  `RouteWeeklyScheduleId` int(11) DEFAULT NULL,
  `CreateDate` datetime NOT NULL,
  `UpdateDate` datetime NOT NULL,
  `InvoiceDate` datetime DEFAULT NULL,
  `TaxPercent` decimal(10,4) DEFAULT NULL,
  `TaxAmount` decimal(10,4) NOT NULL DEFAULT '0.0000',
  `DiscountPercent` decimal(10,4) DEFAULT NULL,
  `DiscountAmount` decimal(10,4) DEFAULT NULL,
  `SubTotal` decimal(10,4) DEFAULT NULL,
  `Total` decimal(10,4) DEFAULT NULL,
  `OrderStatus` varchar(10) NOT NULL,
  `RecordStatus` varchar(10) NOT NULL DEFAULT 'ACT',
  `Note` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `orders`
--

LOCK TABLES `orders` WRITE;
/*!40000 ALTER TABLE `orders` DISABLE KEYS */;
INSERT INTO `orders` VALUES (5,1,1,34,'2018-08-06 12:37:01','2018-09-28 14:15:01','2018-09-28 08:17:26',13.0000,8.5700,25.0000,0.0000,65.9200,74.4900,'INVOICE','ACT',NULL),(6,2,1,38,'2018-10-13 17:44:55','2018-10-13 17:44:55','2018-08-28 00:00:00',13.0000,11.7800,25.0000,0.0000,90.6400,102.4200,'ORDER','ACT',NULL),(7,3,2,0,'2018-10-23 13:42:01','2018-10-23 13:42:01','0001-01-01 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(8,4,2,0,'2018-10-23 13:43:02','2018-10-23 13:43:02','0001-01-01 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(9,5,2,0,'2018-10-23 13:43:35','2018-10-23 13:43:35','0001-01-01 00:00:00',13.0000,1.0700,25.0000,0.0000,8.2400,9.3100,'ORDER','ACT',NULL),(10,6,2,0,'2018-10-23 13:43:51','2018-10-23 13:43:51','0001-01-01 00:00:00',13.0000,1.0700,25.0000,0.0000,8.2400,9.3100,'ORDER','ACT',NULL),(11,7,2,0,'2018-10-23 13:43:53','2018-10-23 13:43:53','0001-01-01 00:00:00',13.0000,1.0700,25.0000,0.0000,8.2400,9.3100,'ORDER','ACT',NULL),(12,8,2,0,'2018-10-23 13:43:54','2018-10-23 13:43:54','0001-01-01 00:00:00',13.0000,1.0700,25.0000,0.0000,8.2400,9.3100,'ORDER','ACT',NULL),(13,9,2,0,'2018-10-23 14:24:14','2018-10-23 14:24:14','2018-10-23 14:20:15',13.0000,1.0700,25.0000,0.0000,8.2400,9.3100,'ORDER','ACT',NULL),(14,10,2,0,'2018-10-23 14:26:18','2018-10-23 14:26:18','2018-10-23 14:25:51',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(15,11,2,0,'2018-10-23 14:31:07','2018-10-23 14:31:07','2018-10-23 14:30:38',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(16,12,2,0,'2018-10-23 15:43:20','2018-10-23 15:43:20','2018-10-23 15:43:05',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(17,13,2,0,'2018-10-23 15:46:47','2018-10-23 15:46:47','2018-10-23 15:46:20',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(18,14,1,0,'2018-10-23 15:50:07','2018-10-23 15:50:07','2018-10-23 15:49:56',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(19,15,1,40,'2018-10-28 10:03:48','2018-10-28 10:03:48','2018-09-04 00:00:00',13.0000,16.0700,25.0000,0.0000,123.6000,139.6700,'ORDER','ACT',NULL),(20,16,2,39,'2018-10-28 10:03:49','2018-10-28 10:03:49','2018-09-03 00:00:00',13.0000,17.1400,25.0000,0.0000,131.8400,148.9800,'ORDER','ACT',NULL),(21,17,3,41,'2018-10-28 10:03:49','2018-10-28 10:03:49','2018-09-05 00:00:00',13.0000,8.0000,30.0000,0.0000,61.5200,69.5200,'ORDER','ACT',NULL),(22,18,1,43,'2018-10-28 10:53:32','2018-10-28 10:53:32','2018-09-11 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(23,19,2,42,'2018-10-28 10:53:33','2018-10-28 10:53:33','2018-09-10 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(24,20,3,44,'2018-10-28 10:53:33','2018-10-28 10:53:33','2018-09-12 00:00:00',13.0000,0.0000,30.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(25,21,3,0,'2018-10-28 11:00:23','2018-10-28 11:00:23','2018-10-28 10:59:34',13.0000,9.9100,30.0000,0.0000,76.2100,86.1200,'ORDER','ACT',NULL),(26,22,1,46,'2018-11-03 21:46:09','2018-11-03 21:46:09','2018-09-18 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(27,23,3,47,'2018-11-03 21:46:09','2018-11-03 21:46:09','2018-09-19 00:00:00',13.0000,0.0000,30.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(28,24,1,49,'2018-12-03 08:36:49','2018-12-03 08:36:49','2018-09-25 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(29,25,2,48,'2018-12-03 08:36:49','2018-12-03 08:36:49','2018-09-24 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(30,26,3,50,'2018-12-03 08:36:49','2018-12-03 08:36:49','2018-09-26 00:00:00',13.0000,0.0000,30.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(31,27,1,52,'2018-12-03 08:49:38','2018-12-07 21:52:53','2018-10-16 00:00:00',13.0000,11.7800,25.0000,0.0000,90.6400,102.4200,'ORDER','ACT',NULL),(32,28,2,51,'2018-12-03 08:49:38','2018-12-03 08:49:38','2018-10-15 00:00:00',13.0000,0.0000,25.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL),(34,29,3,53,'2018-12-07 21:56:03','2018-12-07 21:56:03','2018-10-17 00:00:00',13.0000,0.0000,30.0000,0.0000,0.0000,0.0000,'ORDER','ACT',NULL);
/*!40000 ALTER TABLE `orders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `products`
--

DROP TABLE IF EXISTS `products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `products` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `ProductCode` varchar(10) NOT NULL,
  `Name` text NOT NULL,
  `Description` longtext,
  `Price` decimal(20,2) NOT NULL,
  `RecordStatus` varchar(10) NOT NULL DEFAULT 'ACT',
  `CreateDate` datetime DEFAULT NULL,
  `UpdateDate` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `products`
--

LOCK TABLES `products` WRITE;
/*!40000 ALTER TABLE `products` DISABLE KEYS */;
INSERT INTO `products` VALUES (1,'1001','Single Rose','Single Rose',10.99,'ACT',NULL,'2018-11-11 16:21:34'),(2,'0020','Rose Bouquet','Rose Bouquet',10.99,'ACT',NULL,'2018-07-21 17:56:06'),(3,'0030','Special Bunch','Special Bunch',15.99,'ACT',NULL,'2018-11-03 21:41:44'),(4,'0040','Carnations/Lea leaf','Carnations/Lea leaf',10.99,'ACT',NULL,'2018-07-21 17:57:15'),(5,'0050','Deluxe Bunch','Deluxe Bunch',10.99,'ACT','2018-07-21 18:22:46','2018-07-21 18:22:46');
/*!40000 ALTER TABLE `products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `routeweeklyschedules`
--

DROP TABLE IF EXISTS `routeweeklyschedules`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `routeweeklyschedules` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `routeId` int(11) NOT NULL,
  `DeliveryDate` date NOT NULL,
  `Week` int(11) NOT NULL,
  `SundayOfTheYearId` int(11) NOT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=54 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `routeweeklyschedules`
--

LOCK TABLES `routeweeklyschedules` WRITE;
/*!40000 ALTER TABLE `routeweeklyschedules` DISABLE KEYS */;
INSERT INTO `routeweeklyschedules` VALUES (33,1,'2018-08-13',1,2),(34,2,'2018-08-17',1,2),(35,1,'2018-08-20',2,2),(36,2,'2018-08-21',2,2),(37,1,'2018-08-27',3,2),(38,2,'2018-08-28',3,2),(39,1,'2018-09-03',4,2),(40,2,'2018-09-04',4,2),(41,3,'2018-09-05',4,2),(42,1,'2018-09-10',5,2),(43,2,'2018-09-11',5,2),(44,3,'2018-09-12',5,2),(45,1,'2018-09-17',6,2),(46,2,'2018-09-18',6,2),(47,3,'2018-09-19',6,2),(48,1,'2018-09-24',7,2),(49,2,'2018-09-25',7,2),(50,3,'2018-09-26',7,2),(51,1,'2018-10-15',10,2),(52,2,'2018-10-16',10,2),(53,3,'2018-10-17',10,2);
/*!40000 ALTER TABLE `routeweeklyschedules` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `stores`
--

DROP TABLE IF EXISTS `stores`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `stores` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `BillingInformationId` int(11) NOT NULL,
  `RouteId` int(11) NOT NULL,
  `RouteOrderNo` varchar(45) DEFAULT '0',
  `StoreNumber` varchar(45) DEFAULT '',
  `Status` varchar(10) NOT NULL DEFAULT 'ACT',
  `Address1` varchar(100) DEFAULT NULL,
  `Address2` varchar(100) DEFAULT NULL,
  `City` varchar(45) DEFAULT NULL,
  `State` varchar(45) DEFAULT NULL,
  `Country` varchar(45) DEFAULT NULL,
  `PostalCode` varchar(10) DEFAULT NULL,
  `ContactPerson` varchar(100) DEFAULT NULL,
  `Phone` varchar(45) DEFAULT NULL,
  `Fax` varchar(45) DEFAULT NULL,
  `DateCreated` datetime DEFAULT NULL,
  `DateModified` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `FK_Stores_DeliveryRoutes_RouteId_idx` (`RouteId`),
  KEY `FK_Stores_BillingInformation_BillingInformationId_idx` (`BillingInformationId`),
  CONSTRAINT `FK_Stores_BillingInformation_BillingInformationId` FOREIGN KEY (`BillingInformationId`) REFERENCES `billinginformation` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_Stores_DeliveryRoutes_RouteId` FOREIGN KEY (`RouteId`) REFERENCES `deliveryroutes` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `stores`
--

LOCK TABLES `stores` WRITE;
/*!40000 ALTER TABLE `stores` DISABLE KEYS */;
INSERT INTO `stores` VALUES (1,1,2,'0006','100203','ACT','156 Toronto Rd',NULL,'Port Hope','Ontario',NULL,'L1A 3S5',NULL,NULL,NULL,'2018-07-21 17:52:12','2018-08-12 12:04:25'),(2,1,1,'0010','100205','ACT','234 Calvin st.',NULL,'Port Hope','Ontario',NULL,'L1A 3S5',NULL,NULL,NULL,'2018-08-12 12:01:48','2018-08-26 12:53:45'),(3,3,3,'001',NULL,'ACT','4660 Kingston Rd.',NULL,'Scarborough','ON',NULL,NULL,NULL,NULL,NULL,'2018-10-28 09:54:54','2018-10-28 09:54:54'),(4,1,3,NULL,'123','ACT',' test',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'2018-12-03 09:22:14','2018-12-03 09:22:14');
/*!40000 ALTER TABLE `stores` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `storeweeklyorders`
--

DROP TABLE IF EXISTS `storeweeklyorders`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `storeweeklyorders` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `StoreId` int(11) NOT NULL,
  `ProductId` int(11) NOT NULL,
  `Quantity` int(11) DEFAULT '0',
  `RecordStatus` varchar(10) DEFAULT 'ACT',
  PRIMARY KEY (`Id`),
  KEY `FK_customerweeklyorders_storeid_stores_id_idx` (`StoreId`),
  KEY `FK_customerweeklyorders_productid_products_id_idx` (`ProductId`),
  CONSTRAINT `FK_customerweeklyorders_productid_products_id` FOREIGN KEY (`ProductId`) REFERENCES `products` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `FK_customerweeklyorders_storeid_stores_id` FOREIGN KEY (`StoreId`) REFERENCES `stores` (`Id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `storeweeklyorders`
--

LOCK TABLES `storeweeklyorders` WRITE;
/*!40000 ALTER TABLE `storeweeklyorders` DISABLE KEYS */;
INSERT INTO `storeweeklyorders` VALUES (1,1,1,4,'ACT'),(2,1,2,1,'ACT'),(3,1,5,3,'ACT'),(4,1,5,3,'ACT'),(5,2,1,4,'ACT'),(6,2,1,4,'DEL'),(7,2,1,4,'DEL'),(8,2,1,4,'DEL'),(9,1,1,4,'DEL'),(10,3,1,4,'ACT'),(11,3,2,1,'ACT'),(12,3,5,3,'ACT');
/*!40000 ALTER TABLE `storeweeklyorders` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `sundayoftheyear`
--

DROP TABLE IF EXISTS `sundayoftheyear`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `sundayoftheyear` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `FirstSundayOfTheYear` date NOT NULL,
  `LastDayOfTheYear` date NOT NULL,
  `IsCurrentYear` bit(1) DEFAULT b'0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `sundayoftheyear`
--

LOCK TABLES `sundayoftheyear` WRITE;
/*!40000 ALTER TABLE `sundayoftheyear` DISABLE KEYS */;
INSERT INTO `sundayoftheyear` VALUES (1,'2017-08-06','2018-08-04','\0'),(2,'2018-08-05','2019-08-03',''),(3,'2019-08-04','2020-08-01','\0'),(4,'2020-08-02','2021-01-31','\0'),(5,'2021-08-01','2022-08-06','\0'),(6,'2022-08-07','2023-08-05','\0'),(7,'2023-08-06','2024-08-03','\0'),(8,'2024-08-04','2025-08-02','\0'),(9,'2025-08-03','2026-08-01','\0'),(10,'2026-08-02','2027-07-31','\0'),(11,'2027-08-01','2028-08-05','\0'),(12,'2028-08-06','2029-08-04','\0'),(13,'2029-08-05','2030-08-03','\0'),(14,'2030-08-04','2031-08-02','\0'),(15,'2031-08-03','2032-07-31','\0'),(16,'2032-08-01','2033-08-06','\0'),(17,'2033-08-07','2034-08-05','\0'),(18,'2034-08-06','2035-08-04','\0'),(19,'2035-08-05','2036-08-02','\0'),(20,'2036-08-03','2037-08-01','\0'),(21,'2037-08-02','2038-07-31','\0'),(22,'2038-08-01','2039-08-06','\0'),(23,'2039-08-07','2040-08-04','\0'),(24,'2040-08-05','2041-08-03','\0'),(25,'2041-08-04','2042-08-02','\0'),(26,'2042-08-03','2043-08-01','\0'),(27,'2043-08-02','2044-08-06','\0'),(28,'2044-08-07','2045-08-05','\0'),(29,'2045-08-06','2046-08-04','\0'),(30,'2046-08-05','2047-08-03','\0'),(31,'2047-08-04','2048-08-01','\0'),(32,'2048-08-02','2049-07-31','\0'),(33,'2049-08-01','2050-08-06','\0'),(34,'2050-08-07','2051-08-06','\0');
/*!40000 ALTER TABLE `sundayoftheyear` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'invoicemanager1'
--

--
-- Dumping routines for database 'invoicemanager1'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2018-12-07 22:43:46
