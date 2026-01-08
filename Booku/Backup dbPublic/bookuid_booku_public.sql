-- MySQL dump 10.16  Distrib 10.1.38-MariaDB, for Win32 (AMD64)
--
-- Host: 69.30.235.140    Database: bookuid_booku_public
-- ------------------------------------------------------
-- Server version	8.0.26

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
-- Current Database: `bookuid_booku_public`
--

CREATE DATABASE /*!32312 IF NOT EXISTS*/ `bookuid_booku_public` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;

USE `bookuid_booku_public`;

--
-- Table structure for table `tbl_customer`
--

DROP TABLE IF EXISTS `tbl_customer`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_customer` (
  `Nomor_Seri_Produk` varchar(33) NOT NULL,
  `ID_Customer` varchar(12) NOT NULL,
  `Versi_App` int NOT NULL DEFAULT '0',
  `Apdet_App` int NOT NULL DEFAULT '0',
  `Nama_Perusahaan` varchar(72) NOT NULL,
  `Nama_Direktur` varchar(63) NOT NULL,
  `NPWP` varchar(33) NOT NULL DEFAULT '',
  `Jenis` varchar(45) NOT NULL DEFAULT '',
  `Alamat` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Email` varchar(63) NOT NULL,
  `PIC` varchar(33) NOT NULL,
  `Jumlah_Perangkat` int NOT NULL,
  `Sistem_Approval` varchar(9) DEFAULT NULL,
  `Tahun_Cut_Off` int NOT NULL,
  `Sistem_COA` varchar(33) NOT NULL,
  `Expire` date NOT NULL DEFAULT '1900-01-01',
  PRIMARY KEY (`NPWP`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_customer`
--

LOCK TABLES `tbl_customer` WRITE;
/*!40000 ALTER TABLE `tbl_customer` DISABLE KEYS */;
INSERT INTO `tbl_customer` VALUES ('FT65FR-SFT987-12K9L7-T7KJG0','inrfdred',1,2,'Aris Widodo Consulting','Aris Widodo','070322151408000','Jasa','Perumnas Bumi Telukjambe Blok LA No. 46, Karawang','ariskiplinovic@gmail.com','Aris Widodo',1,'TIDAK',2024,'Standar Aplikasi','2999-02-12'),('F7HU9J-6H5F4D-789ZXC-M7NBC3','dummycli',1,6,'PT. DUMMY CLIENT','Karwan','12345678910','Industri/Manufaktur','Telukbango Batujaya Karawang','dummyclient@gmail.com','Karwan - 081584648530',1,'TIDAK',2021,'Standar Aplikasi','9999-07-10'),('SH2536-GIK25J-P0OIUH-66HH45','lkjhukji',1,2,'PT. COBA APLIKASI','Hasan Sule','1236525252369524','Industri/Manufaktur','Karawang','cobaapp@gmail.com','Hasan Sule',1,'TIDAK',2023,'Standar Aplikasi','2025-03-06'),('KJH425-K58LWU-96GK87-KJ9K8M','mnb87hj9',1,6,'PT. CODING HAMPIR SELESAI','Hasan Sule','1236541252362514','Industri/Manufaktur','Karawang','codingbs@gmail.com','Hasan Sule',1,'TIDAK',2022,'Standar Aplikasi','2025-03-26'),('UJ89KJ-H6G8KO-5GH65R-VBM3FG','ci9kh65g',1,6,'PT. RINDU ORDER','Engkos Kosasih','1239513575236425','Industri/Manufaktur','Karawang','rinduorder@gmail.com','Engkos',1,'TIDAK',2023,'Standar Aplikasi','2025-03-06'),('AR89KJ-LO54RE-128HG6-NG7H56','krgjikjy',1,6,'PT. MIM INDONESIA KONSULTAMA','Aris Widodo','41.986.196.8-416.000','Jasa','Batu Sari Barat, RT. 001 RW. 001, Kel. Batu Sari, Kec. Batuceper, Kota Tangerang, Banten 15121','ptmimindonesiakonsultama@gmail.com','Aris Widodo',1,'TIDAK',2024,'Standar Aplikasi','2999-02-17'),('AK0YT5-BV765G-F2J8E9-127HG5','cbpntgvr',1,6,'PT. SIMULASI PPN','Drs. Atang Kusmana','5213652452365854','Industri/Manufaktur','Karawang','simulasippn@gmail.com','Atang',1,'TIDAK',2024,'Standar Aplikasi','2033-02-11'),('GG98JH-FTJ987-24GHTR-H7G5F3','kitalima',1,6,'PT KITA LIMA BERSAUDARA','MOH ROMLI','86.673.539.2-408.000','Industri/Manufaktur','KARAWANG','fivebrothers.info@gmail.com','MOH ROMLI',1,'TIDAK',2024,'Standar Aplikasi','2026-02-19');
/*!40000 ALTER TABLE `tbl_customer` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_infoaplikasi`
--

DROP TABLE IF EXISTS `tbl_infoaplikasi`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_infoaplikasi` (
  `Nama_Aplikasi` varchar(45) NOT NULL,
  `Versi_App` int NOT NULL,
  `Apdet_App` int NOT NULL,
  `URL_Paket_Booku` varchar(99) NOT NULL,
  `URL_Paket_Installer` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `URL_Paket_Updater` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Folder_Temp_Paket_Booku` varchar(99) NOT NULL,
  `Folder_Temp_Paket_Installer` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Folder_Temp_Paket_Updater` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `File_Paket_Booku` varchar(99) NOT NULL,
  `File_Paket_Installer` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `File_Paket_Updater` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `File_Installer` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `File_Updater` varchar(99) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Nomor_Hotline` varchar(27) NOT NULL,
  `Website` varchar(45) NOT NULL,
  `Email` varchar(45) NOT NULL,
  PRIMARY KEY (`Nama_Aplikasi`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_infoaplikasi`
--

LOCK TABLES `tbl_infoaplikasi` WRITE;
/*!40000 ALTER TABLE `tbl_infoaplikasi` DISABLE KEYS */;
INSERT INTO `tbl_infoaplikasi` VALUES ('BOOKU - Sistem Akuntansi Terpadu',1,7,'https://booku.id/booku/support/PaketBooku.zip','https://booku.id/booku/support/BookuInstaller.zip','https://booku.id/booku/support/BookuUpdater.zip','TempBookuApp','TempInstaller','TempUpdater','PaketBooku.zip','BookuInstaller.zip','BookuUpdater.zip','Booku Installer.exe','Booku Updater.exe','0815-8464-8530','booku.id','');
/*!40000 ALTER TABLE `tbl_infoaplikasi` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_perangkat`
--

DROP TABLE IF EXISTS `tbl_perangkat`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_perangkat` (
  `Nomor_ID` int NOT NULL,
  `ID_Komputer` varchar(33) NOT NULL,
  `Nomor_Seri_Produk` varchar(33) NOT NULL,
  PRIMARY KEY (`Nomor_ID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_perangkat`
--

LOCK TABLES `tbl_perangkat` WRITE;
/*!40000 ALTER TABLE `tbl_perangkat` DISABLE KEYS */;
INSERT INTO `tbl_perangkat` VALUES (1,'BFEBFBFF000906A3','F7HU9J-6H5F4D-789ZXC-M7NBC3'),(4,'BFEBFBFF000906A3','KJH425-K58LWU-96GK87-KJ9K8M'),(7,'BFEBFBFF000306A9','SH2536-GIK25J-P0OIUH-66HH45'),(8,'178BFBFF00A50F00','UJ89KJ-H6G8KO-5GH65R-VBM3FG'),(10,'BFEBFBFF000906A3','AK0YT5-BV765G-F2J8E9-127HG5'),(11,'BFEBFBFF000306A9','FT65FR-SFT987-12K9L7-T7KJG0'),(13,'BFEBFBFF000706A8','AR89KJ-LO54RE-128HG6-NG7H56'),(14,'BFEBFBFF000306D4','GG98JH-FTJ987-24GHTR-H7G5F3');
/*!40000 ALTER TABLE `tbl_perangkat` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_produk`
--

DROP TABLE IF EXISTS `tbl_produk`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_produk` (
  `Nomor_Seri_Produk` varchar(33) NOT NULL,
  `ID_Customer` varchar(17) NOT NULL,
  `Jumlah_Perangkat` int NOT NULL DEFAULT '1',
  `Status_Terpakai` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`Nomor_Seri_Produk`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_produk`
--

LOCK TABLES `tbl_produk` WRITE;
/*!40000 ALTER TABLE `tbl_produk` DISABLE KEYS */;
INSERT INTO `tbl_produk` VALUES ('AK0YT5-BV765G-F2J8E9-127HG5','cbpntgvr',1,1),('AR89KJ-LO54RE-128HG6-NG7H56','krgjikjy',1,1),('F7HU9J-6H5F4D-789ZXC-M7NBC3','dummycli',1,1),('FT65FR-SFT987-12K9L7-T7KJG0','inrfdred',1,1),('GG98JH-FTJ987-24GHTR-H7G5F3','kitalima',1,1),('HY8KH5-K9H56T-1BC6G4-LAD532','jki98ujg',1,0),('KJH425-K58LWU-96GK87-KJ9K8M','mnb87hj9',1,1),('SH2536-GIK25J-P0OIUH-66HH45','lkjhukji',1,1),('UJ89KJ-H6G8KO-5GH65R-VBM3FG','ci9kh65g',1,1);
/*!40000 ALTER TABLE `tbl_produk` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tbl_voucher`
--

DROP TABLE IF EXISTS `tbl_voucher`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tbl_voucher` (
  `Voucher` varchar(63) NOT NULL,
  `Tempo` varchar(27) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tbl_voucher`
--

LOCK TABLES `tbl_voucher` WRITE;
/*!40000 ALTER TABLE `tbl_voucher` DISABLE KEYS */;
/*!40000 ALTER TABLE `tbl_voucher` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Dumping events for database 'bookuid_booku_public'
--

--
-- Dumping routines for database 'bookuid_booku_public'
--
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-02-22 11:40:23
