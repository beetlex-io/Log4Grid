/*
Navicat MySQL Data Transfer

Source Server         : 192.168.0.121_3306
Source Server Version : 50173
Source Host           : 192.168.0.121:3306
Source Database       : log4net

Target Server Type    : MYSQL
Target Server Version : 50173
File Encoding         : 65001

Date: 2014-04-11 17:21:13
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for Application
-- ----------------------------
DROP TABLE IF EXISTS `TBL_Application`;
CREATE TABLE `TBL_Application` (
  `ID` varchar(50) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `Remark` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for Host
-- ----------------------------
DROP TABLE IF EXISTS `TBL_Host`;
CREATE TABLE `TBL_Host` (
  `ID` varchar(50) NOT NULL,
  `AppID` varchar(50) NOT NULL,
  `Name` varchar(255) NOT NULL,
  `CpuUsage` varchar(255) DEFAULT NULL,
  `LastActiveTime` varchar(255) DEFAULT NULL,
  `MemoryUsage` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for Log
-- ----------------------------
DROP TABLE IF EXISTS `TBL_Log`;
CREATE TABLE `TBL_Log` (
  `ID` varchar(50) NOT NULL,
  `Host` varchar(255) DEFAULT NULL,
  `App` varchar(255) DEFAULT NULL,
  `CreateTime` datetime DEFAULT NULL,
  `LogContent` text,
  `Type` int(11) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `createtime_index` (`CreateTime`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;

-- ----------------------------
-- Table structure for User
-- ----------------------------
DROP TABLE IF EXISTS `TBL_User`;
CREATE TABLE `TBL_User` (
  `Name` varchar(50) NOT NULL,
  `User_PWD` varchar(255) DEFAULT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Enabled` int(11) DEFAULT NULL,
  PRIMARY KEY (`Name`)
) ENGINE=MyISAM DEFAULT CHARSET=utf8;
