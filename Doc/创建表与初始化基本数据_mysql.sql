/*
SQLyog Ultimate v11.3 (64 bit)
MySQL - 8.0.3-rc-log : Database - basic_function
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`basic_function` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `basic_function`;

/*Table structure for table `attachment` */

DROP TABLE IF EXISTS `attachment`;

CREATE TABLE `attachment` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `file_name` varchar(50) NOT NULL COMMENT '文件名',
  `expand_name` varchar(10) DEFAULT NULL COMMENT '扩展名',
  `title` varchar(50) DEFAULT NULL COMMENT '标题',
  `file_address` varchar(500) NOT NULL COMMENT '文件地址',
  `file_size` float NOT NULL DEFAULT '0' COMMENT '文件大小（KB）',
  `owner_type` smallint(6) NOT NULL COMMENT '归属类型',
  `owner_id` int(11) DEFAULT NULL COMMENT '归属ID',
  `memo` varchar(500) DEFAULT NULL COMMENT '备注',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8 COMMENT='附件';

/*Data for the table `attachment` */

/*Table structure for table `data_dictionary` */

DROP TABLE IF EXISTS `data_dictionary`;

CREATE TABLE `data_dictionary` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `text` varchar(20) NOT NULL COMMENT '文本',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='数据字典';

/*Data for the table `data_dictionary` */

insert  into `data_dictionary`(`id`,`code`,`text`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,'FlowStatus','流程状态',1,'系统管理员','2019-01-17 16:42:05',1,'系统管理员','2019-01-17 16:42:05'),(2,'BusinessType','业务类型',1,'系统管理员','2019-01-28 17:37:50',1,'系统管理员','2019-01-28 17:37:50'),(3,'FlowHandler','流程处理',1,'系统管理员','2019-01-28 19:47:59',1,'系统管理员','2019-01-28 19:47:59');

/*Table structure for table `data_dictionary_item` */

DROP TABLE IF EXISTS `data_dictionary_item`;

CREATE TABLE `data_dictionary_item` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `code` varchar(20) DEFAULT NULL COMMENT '编码',
  `data_dictionary_id` int(11) NOT NULL COMMENT '数据字典Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `text` varchar(20) NOT NULL COMMENT '文本',
  `system_inlay` tinyint(1) NOT NULL DEFAULT '0' COMMENT '系统内置',
  `expand_table` varchar(100) DEFAULT NULL COMMENT '扩展表',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='数据字典子项';

/*Data for the table `data_dictionary_item` */

insert  into `data_dictionary_item`(`id`,`code`,`data_dictionary_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`text`,`system_inlay`,`expand_table`) values (1,'Draft',1,1,'系统管理员','2019-01-28 21:07:12',1,'系统管理员','2019-01-28 21:10:23','草稿',1,NULL),(2,'Approval',1,1,'系统管理员','2019-01-28 21:07:50',1,'系统管理员','2019-01-28 21:07:50','审批中',1,NULL),(3,'GroupApproval',1,1,'系统管理员','2019-01-28 21:08:03',1,'系统管理员','2019-01-28 21:08:03','集团审批',1,'data_dictionary_item_expand'),(4,'End',1,1,'系统管理员','2019-02-05 08:59:45',1,'系统管理员','2019-02-05 08:59:45','已结束',1,NULL),(9,NULL,2,10,'黄蓉','2019-02-22 22:35:15',1,'系统管理员','2019-02-22 22:36:45','飞亚达',0,NULL),(10,NULL,2,10,'黄蓉','2019-02-22 22:35:23',10,'黄蓉','2019-02-22 22:35:23','中信',0,NULL);

/*Table structure for table `data_dictionary_item_expand` */

DROP TABLE IF EXISTS `data_dictionary_item_expand`;

CREATE TABLE `data_dictionary_item_expand` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `data_dictionary_item_id` int(11) NOT NULL COMMENT '数据字典子项Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `text` varchar(20) NOT NULL COMMENT '文本',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='数据字典子项扩展';

/*Data for the table `data_dictionary_item_expand` */

insert  into `data_dictionary_item_expand`(`id`,`data_dictionary_item_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`name`,`text`) values (1,3,1,'系统管理员','2019-01-28 22:11:59',1,'系统管理员','2019-01-28 22:12:05','总经理','风清扬'),(2,3,1,'系统管理员','2019-01-28 22:12:44',1,'系统管理员','2019-01-28 22:12:52','董事长','黄药师');

/*Table structure for table `flow` */

DROP TABLE IF EXISTS `flow`;

CREATE TABLE `flow` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8 COMMENT='流程';

/*Data for the table `flow` */

insert  into `flow`(`id`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (2,'测试流程审核',1,'系统管理员','2019-08-06 23:07:11',1,'系统管理员','2019-08-06 23:07:17');

/*Table structure for table `flow_censorship` */

DROP TABLE IF EXISTS `flow_censorship`;

CREATE TABLE `flow_censorship` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `flow_id` int(11) NOT NULL COMMENT '流程Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `owner_censorship_type` tinyint(4) NOT NULL COMMENT '{"desc":"归属关卡类型","enum":{"code":"CensorshipType","desc":"关卡类型","items":[{"code":"STANDARD","value":0,"desc":"标准"},{"code":"ROLE","value":1,"desc":"角色"},{"code":"USER","value":2,"desc":"用户"}]}}',
  `owner_censorship_id` int(11) NOT NULL COMMENT '归属关卡ID',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 COMMENT='流程关卡';

/*Data for the table `flow_censorship` */

insert  into `flow_censorship`(`id`,`flow_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`owner_censorship_type`,`owner_censorship_id`) values (7,2,1,'系统管理员','2019-08-06 23:10:55',1,'系统管理员','2019-08-06 23:11:02',0,1),(8,2,1,'系统管理员','2019-08-06 23:12:27',1,'系统管理员','2019-08-06 23:12:31',2,3),(9,2,1,'系统管理员','2019-08-06 23:12:47',1,'系统管理员','2019-08-06 23:12:50',0,3);

/*Table structure for table `form` */

DROP TABLE IF EXISTS `form`;

CREATE TABLE `form` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `form_url` varchar(200) NOT NULL COMMENT '表单URL',
  `form_get_detail_url` varchar(200) NOT NULL COMMENT '表单获取明细URL',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='表单';

/*Data for the table `form` */

insert  into `form`(`id`,`name`,`form_url`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`, `form_get_detail_url`) values (3,'测试表单','/html/Demo/Form/flowForm.html',0,'系统管理员','2019-08-06 23:06:35',1,'1','2019-08-06 23:06:40','/api/TestFormWorkflow/GetFormDetail');

/*Table structure for table `function` */

DROP TABLE IF EXISTS `function`;

CREATE TABLE `function` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='功能';

/*Data for the table `function` */

insert  into `function`(`id`,`code`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,'Query','查询',1,'系统管理员','2019-01-17 15:41:37',1,'系统管理员','2019-01-17 15:41:44'),(2,'Add','添加',1,'系统管理员','2019-01-17 15:42:02',1,'系统管理员','2019-01-17 15:42:12'),(3,'Edit','编辑',1,'系统管理员','2019-01-17 15:42:28',1,'系统管理员','2019-01-17 15:42:36'),(4,'Remove','移除',1,'系统管理员','2019-01-17 15:42:57',1,'系统管理员','2019-01-17 15:43:03'),(5,'ResetPassword','重置密码',1,'系统管理员','2019-02-03 19:38:40',1,'系统管理员','2019-02-03 19:38:43'),(6,'Save','保存',1,'系统管理员','2019-02-04 07:38:25',1,'系统管理员','2019-02-04 07:38:30'),(7,'Upload','上传',1,'系统管理员','2019-03-02 18:41:54',1,'系统管理员','2019-03-02 18:41:58'),(8,'DownLoad','下载',1,'系统管理员','2019-03-02 18:42:27',1,'系统管理员','2019-03-02 18:42:31'),(9,'ImportExcel','导入Excel',1,'系统管理员','2019-03-05 21:40:25',1,'系统管理员','2019-03-05 21:40:29'),(10,'ExportExcel','导出Excel',1,'系统管理员','2019-03-05 21:41:09',1,'系统管理员','2019-03-05 21:41:15'),(11,'Apply','申请',1,'系统管理员','2019-08-06 06:39:46',1,'系统管理员','2019-08-06 06:39:52'),(13,'Audit','审核',1,'系统管理员','2019-08-06 23:47:55',1,'系统管理员','2019-08-06 23:48:05'),(14,'Undo','撤消',1,'系统管理员','2019-08-14 22:04:53',1,'系统管理员','2019-08-14 22:04:58'),(15,'Redo','重做',1,'系统管理员','2019-08-14 22:05:10',1,'系统管理员','2019-08-14 22:05:13'),(16,'ForceRemove','强制删除',1,'系统管理员','2019-08-14 22:08:39',1,'系统管理员','2019-08-14 22:08:45');

/*Table structure for table `menu` */

DROP TABLE IF EXISTS `menu`;

CREATE TABLE `menu` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `link` varchar(200) DEFAULT NULL COMMENT '链接',
  `icon` varchar(20) DEFAULT NULL COMMENT '图标',
  `parent_id` int(11) NOT NULL DEFAULT '0' COMMENT '父ID',
  `sort` int(11) NOT NULL DEFAULT '1' COMMENT '排序',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='菜单';

/*Data for the table `menu` */

insert  into `menu`(`id`,`link`,`icon`,`parent_id`,`sort`,`code`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,NULL,'fa fa-cutlery',0,1,'BasicFunction','基础功能',1,'系统管理员','2019-01-17 15:35:43',1,'系统管理员','2019-01-17 15:35:46'),(2,'/html/BasicFunction/Role/page.html',NULL,1,1,'Role','角色管理',1,'系统管理员','2019-01-17 15:39:39',1,'系统管理员','2019-01-17 15:39:45'),(3,'/html/BasicFunction/User/page.html',NULL,1,2,'User','用户管理',1,'系统管理员','2019-01-17 15:40:19',1,'系统管理员','2019-01-17 15:40:28'),(4,'/html/BasicFunction/PermissionSet/role.html',NULL,1,3,'RolePermission','角色权限',1,'系统管理员','2019-01-28 11:35:04',1,'系统管理员','2019-01-28 11:35:10'),(5,'/html/BasicFunction/DataDictionary/page.html',NULL,1,4,'DataDictionary','数据字典',1,'系统管理员','2019-01-28 17:33:14',1,'系统管理员','2019-01-28 17:33:19'),(6,'/html/BasicFunction/Attachment/page.html',NULL,1,5,'Attachment','附件',1,'系统管理员','2019-03-02 18:41:11',1,'系统管理员','2019-03-02 18:41:19'),(7,'/html/Demo/Form/page.html','fa fa-cutlery',0,1,'TestForm','测试表单',1,'系统管理员','2019-08-06 06:38:31',1,'系统管理员','2019-08-06 06:38:40'),(8,NULL,'fa fa-envelope',0,0,'FlowHandle','流程处理',1,'系统管理员','2019-03-25 00:02:43',1,'系统管理员','2019-03-25 00:02:48'),(9,'/html/FlowHandle/MyWaitFlow/page.html',NULL,8,1,'MyWaitFlow','我的待办',1,'系统管理员','2019-03-25 00:03:18',1,'系统管理员','2019-03-25 00:03:23'),(10,'/html/FlowHandle/MyAuditedFlow/page.html',NULL,8,2,'MyAuditedFlow','我已审核',1,'系统管理员','2019-03-25 00:04:48',1,'系统管理员','2019-03-25 00:04:53'),(11,'/html/FlowHandle/MyApplyFlow/page.html',NULL,8,3,'MyApplyFlow','我的申请',1,'系统管理员','2019-03-25 00:05:46',1,'系统管理员','2019-03-25 00:05:50');

/*Table structure for table `menu_function` */

DROP TABLE IF EXISTS `menu_function`;

CREATE TABLE `menu_function` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `menu_id` int(11) NOT NULL COMMENT '菜单Id',
  `function_id` int(11) NOT NULL COMMENT '功能Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=35 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='菜单功能';

/*Data for the table `menu_function` */

insert  into `menu_function`(`id`,`menu_id`,`function_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,1,1,1,'系统管理员','2019-01-17 15:44:14',1,'系统管理员','2019-01-17 15:43:54'),(2,2,1,1,'系统管理员','2019-01-17 15:45:54',1,'系统管理员','2019-01-17 15:46:00'),(3,2,2,1,'系统管理员','2019-01-17 15:46:15',1,'系统管理员','2019-01-17 15:46:19'),(4,2,3,1,'系统管理员','2019-01-17 15:46:29',1,'系统管理员','2019-01-17 15:46:33'),(5,2,4,1,'系统管理员','2019-01-17 15:46:45',1,'系统管理员','2019-01-17 15:46:50'),(6,3,1,1,'系统管理员','2019-01-17 15:47:00',1,'系统管理员','2019-01-17 15:47:05'),(7,3,2,1,'系统管理员','2019-01-17 15:47:15',1,'系统管理员','2019-01-17 15:47:21'),(8,3,3,1,'系统管理员','2019-01-17 15:47:33',1,'系统管理员','2019-01-17 15:47:37'),(9,3,4,1,'系统管理员','2019-01-17 15:47:48',1,'系统管理员','2019-01-17 15:47:52'),(10,4,1,1,'系统管理员','2019-01-28 11:36:08',1,'系统管理员','2019-01-28 11:36:13'),(11,5,1,1,'系统管理员','2019-01-28 17:34:25',1,'系统管理员','2019-01-28 17:34:31'),(12,5,2,1,'系统管理员','2019-01-28 17:34:46',1,'系统管理员','2019-01-28 17:34:51'),(13,5,3,1,'系统管理员','2019-01-28 17:35:10',1,'系统管理员','2019-01-28 17:35:14'),(14,5,4,1,'系统管理员','2019-01-28 17:35:47',1,'系统管理员','2019-01-28 17:35:51'),(15,3,5,1,'系统管理员','2019-02-03 19:39:20',1,'系统管理员','2019-02-03 19:39:24'),(16,4,1,1,'系统管理员','2019-02-04 07:39:04',1,'系统管理员','2019-02-04 07:39:08'),(17,4,6,1,'系统管理员','2019-02-04 13:05:24',1,'系统管理员','2019-02-04 13:05:31'),(18,6,1,1,'系统管理员','2019-03-02 18:42:59',1,'系统管理员','2019-03-02 18:43:06'),(19,6,4,1,'系统管理员','2019-03-02 18:43:17',1,'系统管理员','2019-03-02 18:43:21'),(20,6,7,1,'系统管理员','2019-03-02 18:43:31',1,'系统管理员','2019-03-02 18:43:35'),(21,6,8,1,'系统管理员','2019-03-02 18:43:51',1,'系统管理员','2019-03-02 18:43:55'),(22,7,6,1,'系统管理员','2019-08-06 06:40:15',1,'系统管理员','2019-08-06 06:40:22'),(23,7,11,1,'系统管理员','2019-08-06 06:40:31',1,'系统管理员','2019-08-06 06:40:35'),(24,7,1,1,'系统管理员','2019-08-06 06:42:11',1,'系统管理员','2019-08-06 06:42:17'),(26,8,1,1,'系统管理员','2019-08-06 23:48:28',1,'系统管理员','2019-08-06 23:48:32'),(27,9,1,1,'系统管理员','2019-08-06 23:48:43',1,'系统管理员','2019-08-06 23:48:46'),(28,9,13,1,'系统管理员','2019-08-06 23:49:06',1,'系统管理员','2019-08-06 23:49:10'),(29,10,1,1,'系统管理员','2019-08-06 23:49:19',1,'系统管理员','2019-08-06 23:49:22'),(30,11,1,1,'系统管理员','2019-08-06 23:49:29',1,'系统管理员','2019-08-06 23:49:35'),(31,7,3,1,'系统管理员','2019-08-07 00:12:04',1,'系统管理员','2019-08-07 00:12:08'),(32,7,4,1,'系统管理员','2019-08-13 23:41:21',1,'系统管理员','2019-08-13 23:41:27'),(33,7,14,1,'系统管理员','2019-08-14 22:09:12',1,'系统管理员','2019-08-14 22:09:16'),(34,7,16,1,'系统管理员','2019-08-14 22:09:25',1,'系统管理员','2019-08-14 22:09:28');

/*Table structure for table `return_flow_route` */

DROP TABLE IF EXISTS `return_flow_route`;

CREATE TABLE `return_flow_route` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `flow_censorship_id` int(11) NOT NULL COMMENT '流程关卡Id',
  `to_flow_censorship_id` int(11) NOT NULL COMMENT '流转到流程关卡ID',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='退件流程路线';

/*Data for the table `return_flow_route` */

insert  into `return_flow_route`(`id`,`flow_censorship_id`,`to_flow_censorship_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (4,8,7,1,'系统管理员','2019-08-21 08:30:52',1,'系统管理员','2019-08-21 08:31:00');

/*Table structure for table `role` */

DROP TABLE IF EXISTS `role`;

CREATE TABLE `role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `system_inlay` tinyint(1) NOT NULL DEFAULT '0' COMMENT '系统内置',
  `system_hide` tinyint(1) NOT NULL DEFAULT '0' COMMENT '系统隐藏',
  `memo` varchar(500) DEFAULT NULL COMMENT '备注',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `code` varchar(20) NOT NULL COMMENT '编码',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=21 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='角色';

/*Data for the table `role` */

insert  into `role`(`id`,`system_inlay`,`system_hide`,`memo`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`code`) values (1,1,0,'系统最高角色','系统管理组',1,'系统管理员','2019-01-25 20:56:27',1,'系统管理员','2019-01-27 23:31:54','100'),(2,1,0,'业务部门的角色','业务组',1,'系统管理员','2019-01-27 11:21:26',1,'系统管理员','2019-01-27 11:21:26','101'),(3,1,0,'技术部门的角色','技术组',1,'系统管理员','2019-01-27 11:22:08',1,'系统管理员','2019-01-27 11:22:08','102'),(16,0,0,'','采购组',1,'系统管理员','2019-02-22 22:29:10',1,'系统管理员','2019-02-22 22:29:10','103'),(17,0,0,'4334','fgdsg',1,'系统管理员','2019-03-02 18:38:06',1,'系统管理员','2019-08-21 13:16:40','104'),(18,0,0,'3232','32',1,'系统管理员','2019-08-21 13:16:59',1,'系统管理员','2019-08-21 13:16:59','3232'),(19,0,0,'4343','4343',1,'系统管理员','2019-08-21 13:17:08',1,'系统管理员','2019-08-21 13:17:08','323243'),(20,0,0,'6464','4564',1,'系统管理员','2019-08-21 13:17:13',1,'系统管理员','2019-08-21 13:17:13','56454');

/*Table structure for table `role_menu_function` */

DROP TABLE IF EXISTS `role_menu_function`;

CREATE TABLE `role_menu_function` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `role_id` int(11) NOT NULL COMMENT '角色Id',
  `menu_function_id` int(11) NOT NULL COMMENT '菜单功能Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=363 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='角色菜单功能';

/*Data for the table `role_menu_function` */

insert  into `role_menu_function`(`id`,`role_id`,`menu_function_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (63,16,1,1,'系统管理员','2019-02-22 22:32:48',1,'系统管理员','2019-02-22 22:32:48'),(64,16,11,1,'系统管理员','2019-02-22 22:32:48',1,'系统管理员','2019-02-22 22:32:48'),(65,16,12,1,'系统管理员','2019-02-22 22:32:48',1,'系统管理员','2019-02-22 22:32:48'),(66,16,13,1,'系统管理员','2019-02-22 22:32:48',1,'系统管理员','2019-02-22 22:32:48'),(67,16,14,1,'系统管理员','2019-02-22 22:32:48',1,'系统管理员','2019-02-22 22:32:48'),(68,3,1,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(69,3,6,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(70,3,7,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(71,3,8,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(72,3,9,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(73,3,15,1,'系统管理员','2019-02-22 22:33:29',1,'系统管理员','2019-02-22 22:33:29'),(259,2,26,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(260,2,27,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(261,2,28,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(262,2,29,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(263,2,30,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(264,2,1,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(265,2,2,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(266,2,3,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(267,2,4,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(268,2,5,1,'系统管理员','2019-08-07 01:43:45',1,'系统管理员','2019-08-07 01:43:45'),(331,1,26,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(332,1,27,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(333,1,28,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(334,1,29,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(335,1,30,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(336,1,22,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(337,1,23,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(338,1,24,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(339,1,31,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(340,1,32,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(341,1,33,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(342,1,34,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(343,1,1,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(344,1,2,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(345,1,3,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(346,1,4,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(347,1,5,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(348,1,6,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(349,1,7,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(350,1,8,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(351,1,9,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(352,1,15,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(353,1,10,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(354,1,17,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(355,1,11,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(356,1,12,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(357,1,13,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(358,1,14,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(359,1,18,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(360,1,19,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(361,1,20,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52'),(362,1,21,1,'系统管理员','2019-08-21 08:24:52',1,'系统管理员','2019-08-21 08:24:52');

/*Table structure for table `send_flow_route` */

DROP TABLE IF EXISTS `send_flow_route`;

CREATE TABLE `send_flow_route` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `flow_censorship_id` int(11) NOT NULL COMMENT '流程关卡Id',
  `to_flow_censorship_id` int(11) NOT NULL COMMENT '流转到流程关卡ID',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8 COMMENT='送件流程路线';

/*Data for the table `send_flow_route` */

insert  into `send_flow_route`(`id`,`flow_censorship_id`,`to_flow_censorship_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (5,7,8,1,'系统管理员','2019-08-06 23:13:31',1,'系统管理员','2019-08-06 23:13:36'),(6,8,9,1,'系统管理员','2019-08-06 23:13:47',1,'系统管理员','2019-08-06 23:13:50');

/*Table structure for table `sequence` */

DROP TABLE IF EXISTS `sequence`;

CREATE TABLE `sequence` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `seq_type` char(2) NOT NULL COMMENT '序列类型',
  `update_date` date NOT NULL COMMENT '更新日期',
  `increment` int(11) NOT NULL DEFAULT '0' COMMENT '增量',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8 COMMENT='序列';

/*Data for the table `sequence` */

insert  into `sequence`(`id`,`seq_type`,`update_date`,`increment`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (4,'AA','2019-08-21',1,1,'系统管理员','2019-08-06 23:04:40',1,'系统管理员','2019-08-21 08:31:18');

/*Table structure for table `standard_censorship` */

DROP TABLE IF EXISTS `standard_censorship`;

CREATE TABLE `standard_censorship` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='标准关卡';

/*Data for the table `standard_censorship` */

insert  into `standard_censorship`(`id`,`code`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,'Applicant','申请者',1,'系统管理员','2019-03-24 00:06:34',1,'系统管理员','2019-03-24 00:06:40'),(2,'Supervisor','上一级主管',1,'系统管理员','2019-04-05 00:12:55',1,'系统管理员','2019-03-24 00:13:01'),(3,'End','结束',1,'系统管理员','2019-03-24 00:13:16',1,'系统管理员','2019-03-24 00:13:20');

/*Table structure for table `test_form` */

DROP TABLE IF EXISTS `test_form`;

CREATE TABLE `test_form` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `code` varchar(20) DEFAULT NULL,
  `name` varchar(20) DEFAULT NULL,
  `apply_no` varchar(50) DEFAULT NULL,
  `flow_status` tinyint(4) DEFAULT NULL,
  `workflow_id` int(11) DEFAULT NULL,
  `creater_id` int(11) DEFAULT NULL,
  `creater` varchar(20) DEFAULT NULL,
  `modifier_id` int(11) DEFAULT NULL,
  `modifier` varchar(20) DEFAULT NULL,
  `create_time` datetime DEFAULT NULL,
  `modify_time` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;

/*Data for the table `test_form` */

insert  into `test_form`(`Id`,`code`,`name`,`apply_no`,`flow_status`,`workflow_id`,`creater_id`,`creater`,`modifier_id`,`modifier`,`create_time`,`modify_time`) values (9,'432','432','AA19081500000',4,90,1,'系统管理员',1,'系统管理员','2019-08-15 00:10:35','2019-08-15 00:11:06'),(10,'45353','543','AA19081500001',1,91,1,'系统管理员',1,'系统管理员','2019-08-15 00:11:42','2019-08-15 00:11:42'),(11,'322','234','AA19081500002',2,92,1,'系统管理员',3,'黄药师','2019-08-15 00:21:22','2019-08-21 08:27:20'),(12,'3232','322','AA19082100000',3,93,1,'系统管理员',3,'黄药师','2019-08-21 08:31:18','2019-08-21 08:32:35');

/*Table structure for table `user` */

DROP TABLE IF EXISTS `user`;

CREATE TABLE `user` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `login_id` varchar(20) NOT NULL COMMENT '登录ID',
  `password` varchar(20) NOT NULL COMMENT '密码',
  `sex` tinyint(1) NOT NULL COMMENT '性别',
  `enabled` tinyint(1) NOT NULL DEFAULT '0' COMMENT '启用',
  `system_inlay` tinyint(1) NOT NULL DEFAULT '0' COMMENT '系统内置',
  `system_hide` tinyint(1) NOT NULL DEFAULT '0' COMMENT '系统隐藏',
  `memo` varchar(500) DEFAULT NULL COMMENT '备注',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `qq` varchar(20) DEFAULT NULL COMMENT 'QQ',
  `wechat` varchar(20) DEFAULT NULL COMMENT '微信',
  `mail` varchar(50) DEFAULT NULL COMMENT '邮箱',
  `mobile` varchar(11) DEFAULT NULL COMMENT '手机',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='用户';

/*Data for the table `user` */

insert  into `user`(`id`,`login_id`,`password`,`sex`,`enabled`,`system_inlay`,`system_hide`,`memo`,`code`,`name`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`qq`,`wechat`,`mail`,`mobile`,`modify_time`) values (1,'system','AC59075B964B0715',1,1,1,0,NULL,'1000','系统管理员',0,'测试用户','2019-01-17 15:28:12',1,'系统管理员',NULL,NULL,NULL,NULL,'2019-02-08 08:15:28'),(3,'huangyaoshi','AC59075B964B0715',1,1,0,0,'','u0001','黄药师',1,'系统管理员','2019-01-27 22:44:41',1,'系统管理员',NULL,NULL,NULL,NULL,'2019-02-22 22:32:00'),(7,'tech','AC59075B964B0715',1,1,0,0,'','u002','任我行',1,'系统管理员','2019-01-28 09:25:38',1,'系统管理员',NULL,NULL,NULL,NULL,'2019-02-22 22:52:46'),(10,'huangrong','AC59075B964B0715',0,1,0,0,'','u00005','黄蓉',7,'任我行','2019-02-22 22:34:06',7,'任我行',NULL,NULL,NULL,NULL,'2019-02-22 22:34:06'),(11,'feasfasfas','9D8A121CE581499D',1,1,0,0,NULL,'sa332','343',1,'系统管理员','2019-08-21 08:23:09',1,'系统管理员',NULL,NULL,NULL,NULL,'2019-08-21 08:24:20');

/*Table structure for table `user_menu_function` */

DROP TABLE IF EXISTS `user_menu_function`;

CREATE TABLE `user_menu_function` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `user_id` int(11) NOT NULL COMMENT '用户Id',
  `menu_function_id` int(11) NOT NULL COMMENT '菜单功能Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='用户菜单功能';

/*Data for the table `user_menu_function` */

insert  into `user_menu_function`(`id`,`user_id`,`menu_function_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,1,1,1,'系统管理员','2019-01-17 15:44:45',1,'系统管理员','2019-01-17 15:44:52'),(2,1,2,1,'系统管理员','2019-01-17 15:48:34',1,'系统管理员','2019-01-17 15:48:39'),(3,1,3,1,'系统管理员','2019-01-17 15:49:22',1,'系统管理员','2019-01-17 15:49:26'),(4,1,4,1,'系统管理员','2019-01-17 15:49:35',1,'系统管理员','2019-01-17 15:49:44'),(5,1,5,1,'系统管理员','2019-01-17 15:49:53',1,'系统管理员','2019-01-17 15:49:57'),(6,1,6,1,'系统管理员','2019-01-17 15:50:12',1,'系统管理员','2019-01-17 15:50:17'),(7,1,7,1,'系统管理员','2019-01-17 15:50:27',1,'系统管理员','2019-01-17 15:50:32'),(8,1,8,1,'系统管理员','2019-01-17 15:50:43',1,'系统管理员','2019-01-17 15:50:47'),(9,1,9,1,'系统管理员','2019-01-17 15:50:59',1,'系统管理员','2019-01-17 15:51:05');

/*Table structure for table `user_role` */

DROP TABLE IF EXISTS `user_role`;

CREATE TABLE `user_role` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `role_id` int(11) NOT NULL COMMENT '角色Id',
  `user_id` int(11) NOT NULL COMMENT '用户Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  PRIMARY KEY (`id`) USING BTREE
) ENGINE=InnoDB AUTO_INCREMENT=23 DEFAULT CHARSET=utf8 ROW_FORMAT=DYNAMIC COMMENT='用户角色';

/*Data for the table `user_role` */

insert  into `user_role`(`id`,`role_id`,`user_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`) values (1,1,1,1,'系统管理员','2019-01-08 20:56:56',1,'系统管理员','2019-01-25 20:57:04'),(18,2,3,1,'系统管理员','2019-02-22 22:30:05',1,'系统管理员','2019-02-22 22:30:05'),(19,3,3,1,'系统管理员','2019-02-22 22:30:05',1,'系统管理员','2019-02-22 22:30:05'),(21,3,7,1,'系统管理员','2019-02-22 22:30:43',1,'系统管理员','2019-02-22 22:30:43'),(22,16,10,7,'任我行','2019-02-22 22:34:06',7,'任我行','2019-02-22 22:34:06');

/*Table structure for table `workflow` */

DROP TABLE IF EXISTS `workflow`;

CREATE TABLE `workflow` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `workflow_define_id` int(11) NOT NULL COMMENT '工作流定义Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `title` varchar(50) NOT NULL COMMENT '标题',
  `apply_no` varchar(20) NOT NULL COMMENT '申请单号',
  `flow_status` tinyint(4) NOT NULL DEFAULT '0' COMMENT '{"desc":"流程状态","enum":{"code":"FlowStatus","desc":"流程状态","items":[{"code":"DRAFT","value":0,"desc":"草稿"},{"code":"AUDITING","value":1,"desc":"审核中"},{"code":"AUDIT_PASS","value":2,"desc":"审核通过"},{"code":"AUDIT_NOPASS","value":3,"desc":"审核驳回"},{"code":"REVERSED","value":4,"desc":"已撤消"}]}}',
  `curr_concrete_censorship_ids` varchar(200) NOT NULL COMMENT '当前流程关卡ID（多个以逗号分隔）',
  `curr_concrete_censorships` varchar(2000) NOT NULL COMMENT '当前流程关卡（多个以逗号分隔）',
  `curr_handler_ids` varchar(200) NOT NULL COMMENT '当前处理人ID（多个以逗号分隔）',
  `curr_handlers` varchar(2000) NOT NULL COMMENT '当前处理人（多个以逗号分隔）',
  `curr_flow_censorship_ids` varchar(200) NOT NULL COMMENT '当前流程关卡ID（多个以逗号分隔）',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=94 DEFAULT CHARSET=utf8 COMMENT='工作流';

/*Data for the table `workflow` */

insert  into `workflow`(`id`,`workflow_define_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`title`,`apply_no`,`flow_status`,`curr_concrete_censorship_ids`,`curr_concrete_censorships`,`curr_handler_ids`,`curr_handlers`,`curr_flow_censorship_ids`) values (90,3,1,'系统管理员','2019-08-15 00:10:35',1,'系统管理员','2019-08-15 00:10:35','23422','AA19081500000',4,'1','申请者','1','系统管理员','7'),(91,3,1,'系统管理员','2019-08-15 00:11:42',1,'系统管理员','2019-08-15 00:11:42','53','AA19081500001',1,'3','黄药师','3','黄药师','8'),(92,3,1,'系统管理员','2019-08-15 00:21:21',3,'黄药师','2019-08-21 08:27:19','2121212','AA19081500002',2,'9','结束','1','系统管理员','9'),(93,3,1,'系统管理员','2019-08-21 08:31:18',3,'黄药师','2019-08-21 08:32:35','3422','AA19082100000',3,'7','申请者','1','系统管理员','7');

/*Table structure for table `workflow_define` */

DROP TABLE IF EXISTS `workflow_define`;

CREATE TABLE `workflow_define` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT '流程Id',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `flow_id` int(11) NOT NULL COMMENT '流程Id',
  `form_id` int(11) NOT NULL COMMENT '表单Id',
  `code` varchar(20) NOT NULL COMMENT '编码',
  `name` varchar(20) NOT NULL COMMENT '名称',
  `enabled` tinyint(1) NOT NULL DEFAULT '0' COMMENT '启用',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8 COMMENT='工作流定义';

/*Data for the table `workflow_define` */

insert  into `workflow_define`(`id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`flow_id`,`form_id`,`code`,`name`,`enabled`) values (3,1,'系统管理员','2019-08-06 23:07:28',1,'系统管理员','2019-08-06 23:07:34',2,3,'AA','测试表单流程',1);

/*Table structure for table `workflow_handle` */

DROP TABLE IF EXISTS `workflow_handle`;

CREATE TABLE `workflow_handle` (
  `id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'Id',
  `workflow_id` int(11) NOT NULL COMMENT '工作流Id',
  `flow_censorship_id` int(11) NOT NULL COMMENT '流程关卡ID',
  `creater_id` int(11) NOT NULL COMMENT '创建人ID',
  `creater` varchar(20) NOT NULL COMMENT '创建人',
  `create_time` datetime NOT NULL COMMENT '创建时间',
  `modifier_id` int(11) NOT NULL COMMENT '修改人ID',
  `modifier` varchar(20) NOT NULL COMMENT '修改人',
  `modify_time` datetime NOT NULL COMMENT '修改时间',
  `handler_id` int(11) NOT NULL COMMENT '处理人ID',
  `handler` varchar(20) NOT NULL COMMENT '处理人',
  `handle_time` datetime DEFAULT NULL COMMENT '处理时间',
  `handle_status` tinyint(4) NOT NULL DEFAULT '0' COMMENT '{"desc":"处理状态","enum":{"code":"HandleStatus","desc":"处理状态","items":[{"code":"UN_HANDLE","value":0,"desc":"未处理"},{"code":"SENDED","value":1,"desc":"已送件"},{"code":"RETURNED","value":2,"desc":"已退件"},{"code":"EFFICACYED","value":3,"desc":"已失效"}]}}',
  `is_readed` tinyint(1) NOT NULL DEFAULT '0' COMMENT '是否已读',
  `idea` varchar(200) DEFAULT NULL COMMENT '意见',
  `handle_type` tinyint(4) NOT NULL DEFAULT '0' COMMENT '{"desc":"处理类型","enum":{"code":"HandleType","desc":"处理类型","items":[{"code":"NOTIFY","value":0,"desc":"通知"},{"code":"AUDIT","value":1,"desc":"审核"},{"code":"APPLY","value":2,"desc":"申请"}]}}',
  `concrete_concrete_id` int(11) NOT NULL COMMENT '具体关卡ID',
  `concrete_concrete` varchar(20) NOT NULL COMMENT '具体关卡',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=290 DEFAULT CHARSET=utf8 COMMENT='工作流处理';

/*Data for the table `workflow_handle` */

insert  into `workflow_handle`(`id`,`workflow_id`,`flow_censorship_id`,`creater_id`,`creater`,`create_time`,`modifier_id`,`modifier`,`modify_time`,`handler_id`,`handler`,`handle_time`,`handle_status`,`is_readed`,`idea`,`handle_type`,`concrete_concrete_id`,`concrete_concrete`) values (280,90,7,1,'系统管理员','2019-08-15 00:10:35',1,'系统管理员','2019-08-15 00:10:35',1,'系统管理员','2019-08-15 00:10:35',1,1,'提交申请',2,1,'申请者'),(282,91,7,1,'系统管理员','2019-08-15 00:11:42',1,'系统管理员','2019-08-15 00:11:42',1,'系统管理员','2019-08-15 00:11:42',1,1,'提交申请',2,1,'申请者'),(283,91,8,1,'系统管理员','2019-08-15 00:11:42',3,'黄药师','2019-08-15 00:11:58',3,'黄药师',NULL,0,1,NULL,1,3,'黄药师'),(284,92,7,1,'系统管理员','2019-08-15 00:21:22',1,'系统管理员','2019-08-21 08:26:47',1,'系统管理员','2019-08-21 08:26:47',1,1,'提交申请',2,1,'申请者'),(285,92,8,1,'系统管理员','2019-08-21 08:26:47',3,'黄药师','2019-08-21 08:27:11',3,'黄药师','2019-08-21 08:27:19',1,1,'送件',1,3,'黄药师'),(286,92,9,3,'黄药师','2019-08-21 08:27:20',3,'黄药师','2019-08-21 08:27:20',1,'系统管理员',NULL,0,0,NULL,0,3,'结束'),(287,93,7,1,'系统管理员','2019-08-21 08:31:18',1,'系统管理员','2019-08-21 08:31:18',1,'系统管理员','2019-08-21 08:31:18',1,1,'提交申请',2,1,'申请者'),(288,93,8,1,'系统管理员','2019-08-21 08:31:18',3,'黄药师','2019-08-21 08:31:39',3,'黄药师','2019-08-21 08:32:35',2,1,'退件',1,3,'黄药师'),(289,93,7,3,'黄药师','2019-08-21 08:32:35',1,'系统管理员','2019-08-21 08:32:53',1,'系统管理员',NULL,0,1,NULL,0,1,'申请者');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
