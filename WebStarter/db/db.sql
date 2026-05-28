/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80018
 Source Host           : localhost:3306
 Source Schema         : winformdevframework

 Target Server Type    : MySQL
 Target Server Version : 80018
 File Encoding         : 65001

 Date: 27/03/2024 18:34:18
*/

SET NAMES utf8mb4;
SET
FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for sysdatasource
-- ----------------------------
DROP TABLE IF EXISTS `sysdatasource`;
CREATE TABLE `sysdatasource`
(
    `ID`           int(11) NOT NULL,
    `ConnectName`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '连接名',
    `Host`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '主机',
    `DataBaseName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '数据库',
    `Username`     varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户名',
    `Password`     varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '密码',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysdatasource
-- ----------------------------
INSERT INTO `sysdatasource`
VALUES (1, '本机', 'localhost', 'winformdevframework', 'sa', '123456');

-- ----------------------------
-- Table structure for sysdicdata
-- ----------------------------
DROP TABLE IF EXISTS `sysdicdata`;
CREATE TABLE `sysdicdata`
(
    `ID`        int(11) NOT NULL,
    `DicTypeID` int(11) NULL DEFAULT NULL COMMENT '字典类型ID',
    `DicData`   varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '内容',
    `Remark`    varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysdicdata
-- ----------------------------
INSERT INTO `sysdicdata`
VALUES (1, 1, 'Menu', '');
INSERT INTO `sysdicdata`
VALUES (2, 1, 'Form', '');
INSERT INTO `sysdicdata`
VALUES (3, 1, 'Button', '');

-- ----------------------------
-- Table structure for sysdictype
-- ----------------------------
DROP TABLE IF EXISTS `sysdictype`;
CREATE TABLE `sysdictype`
(
    `ID`          int(11) NOT NULL,
    `DicTypeName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '字典类型名称',
    `Remark`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    `DicTypeCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '字典类型编码',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysdictype
-- ----------------------------
INSERT INTO `sysdictype`
VALUES (1, '菜单类型', '', 'MenuType');

-- ----------------------------
-- Table structure for sysmenu
-- ----------------------------
DROP TABLE IF EXISTS `sysmenu`;
CREATE TABLE `sysmenu`
(
    `ID`             int(11) NOT NULL COMMENT '菜单ID',
    `ParentID`       int(11) NULL DEFAULT NULL COMMENT '父菜单ID',
    `Title`          varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单标题',
    `URL`            varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单链接地址',
    `Icon`           varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单图标',
    `SortOrder`      int(11) NULL DEFAULT NULL COMMENT '菜单排序顺序',
    `MenuType`       varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单类型',
    `CreatedBy`      int(11) NULL DEFAULT NULL COMMENT '创建人',
    `CreatedDate`    datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
    `UpdatedBy`      int(11) NULL DEFAULT NULL COMMENT '更新者',
    `UpdatedDate`    datetime(0) NULL DEFAULT NULL COMMENT '更新时间',
    `MenuCode`       varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '菜单编码',
    `PermissionCode` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '权限标识',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysmenu
-- ----------------------------
INSERT INTO `sysmenu`
VALUES (1, 0, 'WinformDevFramework', '', 'Resources\\设置.png', 1, 'Menu', NULL, '2023-08-12 13:23:34', 8,
        '2023-08-12 13:23:34', 'SystemSetup', 'SystemSetup');
INSERT INTO `sysmenu`
VALUES (2, 1034, '菜单设置', 'FrmMenu', 'Resources\\菜单.png', 1, 'Form', NULL, '2023-08-12 13:24:06', 8,
        '2023-08-12 13:24:06', 'FrmMenu', 'FrmMenu');
INSERT INTO `sysmenu`
VALUES (3, 1034, '用户设置', 'FrmUser', 'Resources\\用户管理.png', 2, 'Form', NULL, '2023-08-12 13:44:33', 8,
        '2023-08-12 13:44:33', 'FrmUser', 'FrmUser');
INSERT INTO `sysmenu`
VALUES (4, 2, '新增', NULL, NULL, NULL, 'Button', NULL, '2023-08-12 13:45:06', NULL, NULL, 'btnAdd', 'FrmMenu:btnAdd');
INSERT INTO `sysmenu`
VALUES (10, 1034, '角色管理', 'FrmRole', 'Resources\\角色管理.png', 4, 'Form', 8, '2023-08-15 23:43:10', 8,
        '2023-08-15 23:43:10', 'FrmRole', 'FrmRole');
INSERT INTO `sysmenu`
VALUES (11, 2, '保存', NULL, NULL, NULL, 'Button', NULL, '2023-08-16 13:47:57', NULL, NULL, 'btnSave',
        'FrmMenu:btnSave');
INSERT INTO `sysmenu`
VALUES (12, 2, '关闭', '', '', 1, 'Button', 8, '2023-08-16 13:58:39', NULL, NULL, 'btnClose', 'FrmMenu:btnClose');
INSERT INTO `sysmenu`
VALUES (13, 2, '编辑', '', '', 1, 'Button', 8, '2023-08-16 13:59:15', NULL, NULL, 'btnEdit', 'FrmMenu:btnEdit');
INSERT INTO `sysmenu`
VALUES (14, 2, '撤销', '', '', 1, 'Button', 8, '2023-08-16 14:02:19', NULL, NULL, 'btnCanel', 'FrmMenu:btnCanel');
INSERT INTO `sysmenu`
VALUES (15, 2, '删除', '', '', 1, 'Button', 8, '2023-08-16 14:03:13', NULL, NULL, 'btnDel', 'FrmMenu:btnDel');
INSERT INTO `sysmenu`
VALUES (16, 3, '新增', '', '', 1, 'Button', 8, '2023-08-16 14:04:50', NULL, NULL, 'btnAdd', 'FrmUser:btnAdd');
INSERT INTO `sysmenu`
VALUES (17, 3, '保存', '', '', 1, 'Button', 8, '2023-08-16 14:06:47', NULL, NULL, 'btnSave', 'FrmUser:btnSave');
INSERT INTO `sysmenu`
VALUES (18, 3, '关闭', '', '', 1, 'Button', 8, '2023-08-16 14:07:39', NULL, NULL, 'btnClose', 'FrmUser:btnClose');
INSERT INTO `sysmenu`
VALUES (19, 3, '编辑', '', '', 1, 'Button', 8, '2023-08-16 14:08:10', NULL, NULL, 'btnEdit', 'FrmUser:btnEdit');
INSERT INTO `sysmenu`
VALUES (20, 3, '撤销', '', '', 1, 'Button', 8, '2023-08-16 14:08:41', NULL, NULL, 'btnCanel', 'FrmUser:btnCanel');
INSERT INTO `sysmenu`
VALUES (21, 3, '删除', '', '', 1, 'Button', 8, '2023-08-16 14:09:16', NULL, NULL, 'btnDel', 'FrmUser:btnDel');
INSERT INTO `sysmenu`
VALUES (22, 3, '重置密码', '', '', 1, 'Button', 8, '2023-08-16 14:10:16', NULL, NULL, 'btnResetPW',
        'FrmUser:btnResetPW');
INSERT INTO `sysmenu`
VALUES (23, 1034, '数据源维护', 'FrmDataSource', 'Resources\\数据源.png', 5, 'Form', 8, '2023-08-16 17:21:20', 8,
        '2023-08-16 17:21:20', 'FrmDataSource', 'FrmDataSource');
INSERT INTO `sysmenu`
VALUES (24, 23, '新增', '', '', 1, 'Button', 8, '2023-08-16 17:49:00', NULL, NULL, 'btnAdd', 'FrmDataSource:btnAdd');
INSERT INTO `sysmenu`
VALUES (25, 23, '保存', '', '', 1, 'Button', 8, '2023-08-16 17:49:42', NULL, NULL, 'btnSave', 'FrmDataSource:btnSave');
INSERT INTO `sysmenu`
VALUES (26, 1034, '生成代码', 'FrmCreateCode', 'Resources\\生成代码.png', 1, 'Form', 8, '2023-08-16 18:18:51', 8,
        '2023-08-16 18:18:51', 'FrmCreateCode', 'FrmCreateCode');
INSERT INTO `sysmenu`
VALUES (27, 10, '新增', '', '', 1, 'Button', 8, '2023-08-17 10:24:18', NULL, NULL, 'btnAdd', 'FrmRole:btnAdd');
INSERT INTO `sysmenu`
VALUES (28, 10, '保存', '', '', 1, 'Button', 8, '2023-08-17 11:02:07', NULL, NULL, 'btnSave', 'FrmRole:btnSave');
INSERT INTO `sysmenu`
VALUES (29, 10, '关闭', '', '', 1, 'Button', 8, '2023-08-17 12:26:28', NULL, NULL, 'btnClose', 'FrmRole:btnClose');
INSERT INTO `sysmenu`
VALUES (30, 10, '编辑', '', '', 1, 'Button', 8, '2023-08-17 14:12:53', NULL, NULL, 'btnEdit', 'FrmRole:btnEdit');
INSERT INTO `sysmenu`
VALUES (31, 10, '撤销', '', '', 1, 'Button', 8, '2023-08-17 14:13:29', NULL, NULL, 'btnCanel', 'FrmRole:btnCanel');
INSERT INTO `sysmenu`
VALUES (32, 10, '删除', '', '', 1, 'Button', 8, '2023-08-17 14:14:26', NULL, NULL, 'btnDel', 'FrmRole:btnDel');
INSERT INTO `sysmenu`
VALUES (33, 1034, '消息通知', 'FrmMessage', 'Resources\\消息.png', 1, 'Form', 8, '2023-08-17 16:10:11', 8,
        '2023-08-17 16:10:11', 'FrmMessage', 'FrmMessage');
INSERT INTO `sysmenu`
VALUES (34, 1034, '测试', 'Form1', 'Resources\\测试.png', 1, 'Form', 8, '2023-08-17 23:55:07', 8, '2023-08-17 23:55:07',
        'Form1', 'Form1');
INSERT INTO `sysmenu`
VALUES (1033, 1, '基础资料', '', 'Resources\\基础资料.png', 1, 'Menu', 8, '2023-08-18 12:39:01', 8,
        '2023-08-18 12:39:01', 'BaseData', 'BaseData');
INSERT INTO `sysmenu`
VALUES (1034, 1, '系统设置', '', 'Resources\\设置.png', 1, 'Menu', NULL, '2023-08-12 13:23:34', 8,
        '2023-08-12 13:23:34', 'SystemSetup', 'SystemSetup');
INSERT INTO `sysmenu`
VALUES (1035, 1034, '字典类型', 'FrmDicType', 'Resources\\字典类型.png', 1, 'Form', 8, '2023-08-18 14:30:07', 8,
        '2023-08-18 14:30:07', 'FrmDicType', 'FrmDicType');
INSERT INTO `sysmenu`
VALUES (1036, 1034, '字典内容', 'FrmDicData', 'Resources\\字典内容.png', 1, 'Form', 8, '2023-08-18 15:42:05', NULL,
        NULL, 'FrmDicData', 'FrmDicData');
INSERT INTO `sysmenu`
VALUES (1037, 23, '编辑', '', '', 1, 'Button', 8, '2023-08-18 19:12:01', 8, '2023-08-18 19:12:01', 'btnEdit',
        'FrmDataSource:btnEdit');
INSERT INTO `sysmenu`
VALUES (1038, 23, '撤销', '', '', 1, 'Button', 8, '2023-08-18 19:15:03', NULL, NULL, 'btnCanel',
        'FrmDataSource:btnCanel');
INSERT INTO `sysmenu`
VALUES (1039, 23, '删除', '', '', 1, 'Button', 8, '2023-08-18 19:16:07', NULL, NULL, 'btnDel', 'FrmDataSource:btnDel');
INSERT INTO `sysmenu`
VALUES (1040, 23, '关闭', '', '', 1, 'Button', 8, '2023-08-18 19:16:42', NULL, NULL, 'btnClose',
        'FrmDataSource:btnClose');
INSERT INTO `sysmenu`
VALUES (2033, 1033, '供应商信息', 'FrmSupplier',
        'C:\\Users\\64842\\Desktop\\代码开发框架\\WinformGeneralDeveloperFrame\\WinformDevFramework\\Resources\\供应商.png',
        1, 'Form', 8, '2023-08-22 12:12:21', NULL, NULL, 'FrmSupplier', 'FrmSupplier');

-- ----------------------------
-- Table structure for sysmessage
-- ----------------------------
DROP TABLE IF EXISTS `sysmessage`;
CREATE TABLE `sysmessage`
(
    `ID`         int(11) NOT NULL,
    `Title`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '消息标题',
    `Content`    text CHARACTER SET utf8 COLLATE utf8_general_ci NULL COMMENT '消息内容',
    `FromUserID` int(11) NULL DEFAULT NULL COMMENT '发送人',
    `ToUserID`   int(11) NULL DEFAULT NULL COMMENT '接收人',
    `IsSend`     tinyint(4) NULL DEFAULT NULL COMMENT '是否发送',
    `CreateTime` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
    `SendTime`   datetime(0) NULL DEFAULT NULL COMMENT '发送时间',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysmessage
-- ----------------------------
INSERT INTO `sysmessage`
VALUES (3, '测试', '这是一条测试消息', 8, 8, 1, '2023-08-17 21:35:50', '2023-08-17 21:52:56');
INSERT INTO `sysmessage`
VALUES (4, '11', '11发发', 8, 8, 1, '2023-08-17 21:53:11', '2023-08-17 21:53:12');
INSERT INTO `sysmessage`
VALUES (5, '11', '11发发', 8, 8, 1, '2023-08-17 21:53:21', '2023-08-17 21:53:22');
INSERT INTO `sysmessage`
VALUES (6, '11', '11发发', 8, 8, 1, '2023-08-17 21:53:24', '2023-08-17 21:53:25');
INSERT INTO `sysmessage`
VALUES (7, '11', '11发发', 8, 8, 1, '2023-08-17 21:53:26', '2023-08-17 21:53:27');
INSERT INTO `sysmessage`
VALUES (8, '11', '11发发', 8, 8, 1, '2023-08-17 21:53:28', '2023-08-17 21:53:28');
INSERT INTO `sysmessage`
VALUES (9, '11', '11发发', 8, 10, 1, '2023-08-17 21:53:59', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (10, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:01', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (11, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:03', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (12, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:04', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (13, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:05', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (14, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:07', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (15, '11', '11发发', 8, 10, 1, '2023-08-17 21:54:09', '2023-08-17 21:55:16');
INSERT INTO `sysmessage`
VALUES (16, '来自王凯的消息', '达瓦达瓦服务器服务器', 8, 10, 1, '2023-08-17 21:57:55', '2023-08-17 21:57:55');
INSERT INTO `sysmessage`
VALUES (17, '来自王凯的消息', '达瓦达瓦服务器服务器', 8, 8, 1, '2023-08-17 22:02:39', '2023-08-17 22:02:40');
INSERT INTO `sysmessage`
VALUES (18, '来自王凯的消息', '达瓦达瓦服务器服务器', 8, 10, 1, '2023-08-17 22:02:39', '2023-08-17 22:02:39');
INSERT INTO `sysmessage`
VALUES (19, '来自王凯的消息', '达瓦达瓦服务器服务器', 8, 11, 1, '2023-08-17 22:02:39', '2023-08-18 00:29:47');
INSERT INTO `sysmessage`
VALUES (1003, '来自王凯的消息', '这是一条测试消息', 8, 11, 1, '2023-08-18 00:30:45', '2023-08-18 00:30:27');
INSERT INTO `sysmessage`
VALUES (2003, '111', '111', 8, 8, 1, '2023-08-18 10:21:22', '2023-08-18 10:21:22');
INSERT INTO `sysmessage`
VALUES (2004, '111', '111', 8, 10, 0, '2023-08-18 10:21:22', NULL);
INSERT INTO `sysmessage`
VALUES (2005, '111', '111', 8, 11, 0, '2023-08-18 10:21:22', NULL);
INSERT INTO `sysmessage`
VALUES (2006, '111',
        '速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给',
        8, 8, 1, '2023-08-18 12:38:28', '2023-08-18 12:38:28');
INSERT INTO `sysmessage`
VALUES (2007, '111',
        '速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给',
        8, 8, 1, '2023-08-18 12:38:37', '2023-08-18 12:38:38');
INSERT INTO `sysmessage`
VALUES (2008, '111',
        '速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给',
        8, 10, 0, '2023-08-18 12:38:37', NULL);
INSERT INTO `sysmessage`
VALUES (2009, '111',
        '速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给速度跟不上的过半数的根本VS的改变VS E给',
        8, 11, 0, '2023-08-18 12:38:37', NULL);
INSERT INTO `sysmessage`
VALUES (2010, '程序更新', '程序有新版本了，重新启动即可自动更新', 8, 8, 1, '2023-08-19 01:13:46', '2023-08-19 01:13:46');
INSERT INTO `sysmessage`
VALUES (3003, '测试', '我是一条测试消息', 8, 8, 1, '2023-08-19 22:53:06', '2023-08-19 22:53:07');
INSERT INTO `sysmessage`
VALUES (3004, '测试', '我是一条测试消息', 8, 8, 1, '2023-08-20 00:27:18', '2023-08-20 00:27:18');
INSERT INTO `sysmessage`
VALUES (3005, '1', '1', 8, 8, 1, '2023-08-22 10:46:57', '2023-08-22 10:46:58');
INSERT INTO `sysmessage`
VALUES (3006, '333', '333', 8, 8, 1, '2023-08-22 19:03:16', '2023-08-22 19:03:17');
INSERT INTO `sysmessage`
VALUES (3007, '333', '333', 8, 10, 0, '2023-08-22 19:03:16', NULL);
INSERT INTO `sysmessage`
VALUES (3008, '333', '333', 8, 11, 0, '2023-08-22 19:03:16', NULL);
INSERT INTO `sysmessage`
VALUES (4005, '1', '1', 8, 8, 1, '2023-08-28 15:06:02', '2023-08-28 15:06:02');

-- ----------------------------
-- Table structure for sysrole
-- ----------------------------
DROP TABLE IF EXISTS `sysrole`;
CREATE TABLE `sysrole`
(
    `ID`         int(11) NOT NULL,
    `RoleName`   varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '角色名称',
    `Remark`     varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    `CreateBy`   int(11) NULL DEFAULT NULL COMMENT '创建人',
    `CreateTime` datetime(0) NULL DEFAULT NULL COMMENT '创建时间',
    `UpdateBy`   int(11) NULL DEFAULT NULL COMMENT '修改人',
    `UpdateTime` datetime(0) NULL DEFAULT NULL COMMENT '修改时间',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysrole
-- ----------------------------
INSERT INTO `sysrole`
VALUES (1, '管理员', '', 1, '2023-08-17 10:21:56', 8, '2023-08-22 12:15:56');
INSERT INTO `sysrole`
VALUES (9, '开发', '', 8, '2023-08-17 15:16:33', NULL, NULL);

-- ----------------------------
-- Table structure for sysrolemenu
-- ----------------------------
DROP TABLE IF EXISTS `sysrolemenu`;
CREATE TABLE `sysrolemenu`
(
    `RoleID` int(11) NULL DEFAULT NULL COMMENT '角色ID',
    `MenuID` int(11) NULL DEFAULT NULL COMMENT '菜单ID'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysrolemenu
-- ----------------------------
INSERT INTO `sysrolemenu`
VALUES (1, 1);
INSERT INTO `sysrolemenu`
VALUES (1, 1033);
INSERT INTO `sysrolemenu`
VALUES (1, 2033);
INSERT INTO `sysrolemenu`
VALUES (1, 1034);
INSERT INTO `sysrolemenu`
VALUES (1, 2);
INSERT INTO `sysrolemenu`
VALUES (1, 4);
INSERT INTO `sysrolemenu`
VALUES (1, 11);
INSERT INTO `sysrolemenu`
VALUES (1, 12);
INSERT INTO `sysrolemenu`
VALUES (1, 13);
INSERT INTO `sysrolemenu`
VALUES (1, 14);
INSERT INTO `sysrolemenu`
VALUES (1, 15);
INSERT INTO `sysrolemenu`
VALUES (1, 3);
INSERT INTO `sysrolemenu`
VALUES (1, 16);
INSERT INTO `sysrolemenu`
VALUES (1, 17);
INSERT INTO `sysrolemenu`
VALUES (1, 18);
INSERT INTO `sysrolemenu`
VALUES (1, 19);
INSERT INTO `sysrolemenu`
VALUES (1, 20);
INSERT INTO `sysrolemenu`
VALUES (1, 21);
INSERT INTO `sysrolemenu`
VALUES (1, 22);
INSERT INTO `sysrolemenu`
VALUES (1, 10);
INSERT INTO `sysrolemenu`
VALUES (1, 27);
INSERT INTO `sysrolemenu`
VALUES (1, 28);
INSERT INTO `sysrolemenu`
VALUES (1, 29);
INSERT INTO `sysrolemenu`
VALUES (1, 30);
INSERT INTO `sysrolemenu`
VALUES (1, 31);
INSERT INTO `sysrolemenu`
VALUES (9, 1);
INSERT INTO `sysrolemenu`
VALUES (9, 2);
INSERT INTO `sysrolemenu`
VALUES (9, 4);
INSERT INTO `sysrolemenu`
VALUES (9, 11);
INSERT INTO `sysrolemenu`
VALUES (9, 12);
INSERT INTO `sysrolemenu`
VALUES (9, 13);
INSERT INTO `sysrolemenu`
VALUES (9, 14);
INSERT INTO `sysrolemenu`
VALUES (9, 15);
INSERT INTO `sysrolemenu`
VALUES (9, 23);
INSERT INTO `sysrolemenu`
VALUES (9, 24);
INSERT INTO `sysrolemenu`
VALUES (9, 25);
INSERT INTO `sysrolemenu`
VALUES (9, 26);
INSERT INTO `sysrolemenu`
VALUES (1, 32);
INSERT INTO `sysrolemenu`
VALUES (1, 23);
INSERT INTO `sysrolemenu`
VALUES (1, 24);
INSERT INTO `sysrolemenu`
VALUES (1, 25);
INSERT INTO `sysrolemenu`
VALUES (1, 1037);
INSERT INTO `sysrolemenu`
VALUES (1, 1038);
INSERT INTO `sysrolemenu`
VALUES (1, 1039);
INSERT INTO `sysrolemenu`
VALUES (1, 1040);
INSERT INTO `sysrolemenu`
VALUES (1, 26);
INSERT INTO `sysrolemenu`
VALUES (1, 33);
INSERT INTO `sysrolemenu`
VALUES (1, 34);
INSERT INTO `sysrolemenu`
VALUES (1, 1035);
INSERT INTO `sysrolemenu`
VALUES (1, 1036);

-- ----------------------------
-- Table structure for sysuser
-- ----------------------------
DROP TABLE IF EXISTS `sysuser`;
CREATE TABLE `sysuser`
(
    `ID`            int(11) NOT NULL COMMENT '唯一标识用户的主键',
    `Username`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户登录名',
    `Password`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户登录密码',
    `Fullname`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户姓名',
    `Email`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户电子邮件',
    `PhoneNumber`   varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '用户手机号码',
    `CreateTime`    datetime(0) NULL DEFAULT NULL COMMENT '账号创建时间',
    `LastLoginTime` datetime(0) NULL DEFAULT NULL COMMENT '最后登陆时间',
    `Status`        tinyint(4) NULL DEFAULT NULL COMMENT '用户状态 启用 禁用',
    `Sex`           varchar(10) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '性别',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysuser
-- ----------------------------
INSERT INTO `sysuser`
VALUES (8, 'wangkai', '123456', '王凯', '648428741@qq.com', '15029367414', '2023-08-11 15:14:08', '2023-08-28 17:37:27',
        1, '男');
INSERT INTO `sysuser`
VALUES (10, 'admin', '123456', '管理员', '', '', '2023-08-17 12:44:51', '2023-08-17 21:55:15', 1, '男');
INSERT INTO `sysuser`
VALUES (11, 'test', '123456', '测试', '', '', '2023-08-17 12:46:24', '2023-08-18 00:29:46', 1, '男');

-- ----------------------------
-- Table structure for sysuserrole
-- ----------------------------
DROP TABLE IF EXISTS `sysuserrole`;
CREATE TABLE `sysuserrole`
(
    `UserID` int(11) NULL DEFAULT NULL COMMENT '用户ID',
    `RoleID` int(11) NULL DEFAULT NULL COMMENT '角色ID'
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of sysuserrole
-- ----------------------------
INSERT INTO `sysuserrole`
VALUES (8, 1);
INSERT INTO `sysuserrole`
VALUES (11, 9);

-- ----------------------------
-- Table structure for w_customer
-- ----------------------------
DROP TABLE IF EXISTS `w_customer`;
CREATE TABLE `w_customer`
(
    `ID`             int(11) NOT NULL,
    `SupplierName`   varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
    `ContactPerson`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系人',
    `PhoneNumber`    varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号码',
    `Email`          varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
    `BeginCollect`   decimal(18, 2) NULL DEFAULT NULL COMMENT '期初应收',
    `EndPayCollect`  decimal(18, 2) NULL DEFAULT NULL COMMENT '期末应收',
    `TaxtRate`       decimal(18, 4) NULL DEFAULT NULL COMMENT '税率',
    `SortOrder`      int(11) NULL DEFAULT NULL COMMENT '排序',
    `Status`         tinyint(4) NULL DEFAULT NULL COMMENT '状态',
    `Remark`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    `Faxing`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '传真',
    `Address`        varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
    `Bank`           varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '开户行',
    `TaxpayerNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '纳税人识别号',
    `BankAccount`    varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '银行账号',
    `LandlinePhone`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '固定电话',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '客户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_customer
-- ----------------------------

-- ----------------------------
-- Table structure for w_handlermanger
-- ----------------------------
DROP TABLE IF EXISTS `w_handlermanger`;
CREATE TABLE `w_handlermanger`
(
    `ID`        int(11) NOT NULL,
    `UserID`    int(11) NULL DEFAULT NULL COMMENT '人员ID',
    `Type`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类型',
    `Status`    tinyint(4) NULL DEFAULT NULL COMMENT '状态',
    `SortOrder` int(11) NULL DEFAULT NULL COMMENT '排序',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '经手人管理' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_handlermanger
-- ----------------------------

-- ----------------------------
-- Table structure for w_project
-- ----------------------------
DROP TABLE IF EXISTS `w_project`;
CREATE TABLE `w_project`
(
    `ID`          int(11) NOT NULL,
    `ProjectName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '收支项目',
    `ProjectType` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '类型',
    `SortOrder`   int(11) NULL DEFAULT NULL COMMENT '排序',
    `Remark`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '收支项目' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_project
-- ----------------------------

-- ----------------------------
-- Table structure for w_settlementaccount
-- ----------------------------
DROP TABLE IF EXISTS `w_settlementaccount`;
CREATE TABLE `w_settlementaccount`
(
    `ID`          int(11) NOT NULL,
    `Code`        varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '编号',
    `Name`        varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
    `BeginAmount` decimal(18, 2) NULL DEFAULT NULL COMMENT '期初金额',
    `NowAmount`   decimal(18, 2) NULL DEFAULT NULL COMMENT '当前余额',
    `SortOrder`   int(11) NULL DEFAULT NULL COMMENT '排序',
    `Remark`      varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '结算账户' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_settlementaccount
-- ----------------------------

-- ----------------------------
-- Table structure for w_supplier
-- ----------------------------
DROP TABLE IF EXISTS `w_supplier`;
CREATE TABLE `w_supplier`
(
    `ID`             int(11) NOT NULL,
    `SupplierName`   varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '名称',
    `ContactPerson`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '联系人',
    `PhoneNumber`    varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '手机号码',
    `Email`          varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '邮箱',
    `BeginPay`       decimal(18, 2) NULL DEFAULT NULL COMMENT '期初应付',
    `EndPay`         decimal(18, 2) NULL DEFAULT NULL COMMENT '期末应付',
    `TaxtRate`       decimal(18, 4) NULL DEFAULT NULL COMMENT '税率',
    `SortOrder`      int(11) NULL DEFAULT NULL COMMENT '排序',
    `Status`         tinyint(4) NULL DEFAULT NULL COMMENT '状态',
    `Remark`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    `Faxing`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '传真',
    `Address`        varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
    `Bank`           varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '开户行',
    `TaxpayerNumber` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '纳税人识别号',
    `BankAccount`    varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '银行账号',
    `LandlinePhone`  varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '固定电话',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '供应商' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_supplier
-- ----------------------------

-- ----------------------------
-- Table structure for w_warehourses
-- ----------------------------
DROP TABLE IF EXISTS `w_warehourses`;
CREATE TABLE `w_warehourses`
(
    `ID`             int(11) NOT NULL,
    `WarehousesName` varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '仓库名称',
    `Address`        varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '地址',
    `StorageFee`     decimal(18, 2) NULL DEFAULT NULL COMMENT '仓储费',
    `ChargePersonID` int(11) NULL DEFAULT NULL COMMENT '负责人ID',
    `SortOrder`      int(11) NULL DEFAULT NULL COMMENT '排序',
    `Remark`         varchar(255) CHARACTER SET utf8 COLLATE utf8_general_ci NULL DEFAULT NULL COMMENT '备注',
    PRIMARY KEY (`ID`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8 COLLATE = utf8_general_ci COMMENT = '仓库' ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of w_warehourses
-- ----------------------------

SET
FOREIGN_KEY_CHECKS = 1;
