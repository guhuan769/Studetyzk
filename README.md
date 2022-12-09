DI包 Microsoft.Extensions.DependencyInjection
GIT 指定分支拉去代码语法 git clone -b gh https://github.com/guhuan769/Studetyzk.git   
## 目录  
[Studetyzk](#Studetyzk)  
[EFCORE如何通用查看SQL语句](#EFCORE如何通用查看SQL语句)  
[EFCORE生成不同数据库的Migration](#EFCORE生成不同数据库的Migration)  
[Mysql使用EF](#Mysql使用EF)
[目前数据库用的最多的](#目前数据库用的最多的)  
[数据库优化](#数据库优化)  
[EFCore性能](#EFCore性能)
# Studetyzk

LLinq 分组 投影 计算平均工资
![image](https://user-images.githubusercontent.com/46043439/205556966-5f48b9a8-56e8-4304-93a3-ed765c867c29.png)  
IOC  
![image](https://user-images.githubusercontent.com/46043439/205588560-83ceacfe-31fd-4b5c-80cc-00a4774f906c.png)  
IOC  
生命周期  
![image](https://user-images.githubusercontent.com/46043439/205592916-588efe92-9c71-4483-b4a9-3fb5750b080e.png)  

![image](https://user-images.githubusercontent.com/46043439/205600600-3fb0457d-87cd-441e-8f4a-e332aae4ea5e.png)  

![image](https://user-images.githubusercontent.com/46043439/206098817-9fca54f0-3b6a-446b-95e0-294fce3dc15c.png)  


GIT问题 fatal: unable to access 'https://github.com/guhuan769/Student.git/': OpenSSL SSL_connect: Connection was reset in connection to github.com:443  
解决 https://blog.csdn.net/philosophyatmath/article/details/125070079 该问题是VPN可能自动将代理给更改了 只需要手动该城代理端口语法如下  
git config --global http.proxy 127.0.0.1:7890
git config --global https.proxy 127.0.0.1:7890
什么是ORM  
 ![image](https://user-images.githubusercontent.com/46043439/206098869-e202f690-bb21-4fec-8f0a-f9c9d1a9c735.png)  

FluentAPI  
![image](https://user-images.githubusercontent.com/46043439/206351720-e5079166-d9cd-437e-aa4d-739952597bb6.png)  
加快数据得查询速度用复合索引  聚集索引:物理存储按照索引排序非聚集索引:物理存储不按照索引排序优势与缺点聚集索引：插入数据时速度要慢（时间花费在“物理存储的排序”上，也就是首先要找到位置然后插入查询数据比非聚集数据的速度快  
1、对于复合索引,在查询使用时,最好将条件顺序按找索引的顺序,这样效率最高;     select * from table1 where col1=A AND col2=B AND col3=D     如果使用 where col2=B AND col1=A 或者 where col2=B 将不会使用索引
    2、何时是用复合索引     根据where条件建索引是极其重要的一个原则;     注意不要过多用索引,否则对表更新的效率有很大的影响,因为在操作表的时候要化大量时间花在创建索引中

    3、复合索引会替代单一索引么     如果索引满足窄索引的情况下可以建立复合索引,这样可以节约空间和时间

备注:     对一张表来说,如果有一个复合索引 on   (col1,col2),就没有必要同时建立一个单索引 on col1;     如果查询条件需要,可以在已有单索引 on col1的情况下,添加复合索引on (col1,col2),对于效率有一定的提高     同时建立多字段(包含5、6个字段)的复合索引没有特别多的好处,相对而言,建立多个窄字段(仅包含一个,或顶多2个字段)的索引可以达到更好的效率和灵活性  
![image](https://user-images.githubusercontent.com/46043439/206356786-1649db39-d53a-4a1f-894b-eb4ec7b21227.png)  
自动增长得字段并发性能很差 
![image](https://user-images.githubusercontent.com/46043439/206362869-d053807f-0874-46a4-9bae-f4534f5011c9.png)  
guid 如果设置为聚集索引得话 查询效率会非常低  Mysql ID 如果用GUID 设置为聚集索引得话会死得很惨咳咳咳....  
总结SQLSERVER中可以用GUID 但不要使用聚集索引 MYSQL彻底不要用GUID  
![image](https://user-images.githubusercontent.com/46043439/206364789-42029699-cb21-4a0e-a621-c40b3561b6c5.png)  
 
# EFCORE如何通用查看SQL语句  
## 支持所有数据库ORM框架查看SQL  
   1 标准日志 
   ![image](https://user-images.githubusercontent.com/46043439/206613293-2fa8ed0d-85d3-48a4-994c-447d97d3d723.png)  
   2 简单日志  
   ![image](https://user-images.githubusercontent.com/46043439/206614866-d719c611-085f-46ff-8b84-5dc147eb185e.png)  
   ![image](https://user-images.githubusercontent.com/46043439/206616744-cf247988-659f-4b65-a01d-3ca2a899387e.png)  
   以上功能 对应EF 项目 EFCORETEST2可以查看详细操作  
#  EFCORE生成不同数据库的Migration  
![image](https://user-images.githubusercontent.com/46043439/206617263-c71d3f97-c31a-4cdd-b355-e3097ee4105b.png)  
![image](https://user-images.githubusercontent.com/46043439/206618228-1869e2f5-7dd5-4c50-825c-f662449422fb.png)  

# Mysql使用EF
 只需要嵌入Pomelo.EntityFrameworkCore.Mysql该 NUGET包即可
![image](https://user-images.githubusercontent.com/46043439/206619037-cd5e8c08-0bb6-43a7-ab2d-a5141d59c871.png)
# 目前数据库用的最多的  
  postgresql(该数据库开发者 主要是微软官方的主程序员开发制作Npgsql) 安装: Install-Package Npgsql.EntityFrameworkCore.PostgreSQL  NuGet\Install-Package Npgsql -Version 7.0.0 下载本地包:https://www.nuget.org/packages/Npgsql.EntityFrameworkCore.PostgreSQL  
  Mysql 由于被Oracle收购 出了社区版与收费版所以目前用的少 传统数据库 Oracle 很少人用了 sqlserver   
  ![image](https://user-images.githubusercontent.com/46043439/206620410-181d96cd-a420-433c-b975-876aadd25c2d.png)  

#  数据库优化
1  在查询的时候经量不要用select * from xx 去查询 一定要用什么字段写什么字段 select ID,NAME FROM DUAL 
EF 设置外键属性  
![image](https://user-images.githubusercontent.com/46043439/206647834-cfa2897b-e0f0-49cc-a1fa-ed29aaa49cca.png)  

# EFCore性能  
 重:大部分的查询比绝大多数的程序员写出的SQL 效率要高
    少部分可能不那么效率,但影响不大 特殊的SQL语句可能影响瓶顶,此时就需要特殊优化 (优化方法或者手写SQL语句都OK)  
![image](https://user-images.githubusercontent.com/46043439/206653317-c288c6a7-3fd8-4a4c-80d4-6cea557b51d5.png)  
![image](https://user-images.githubusercontent.com/46043439/206653932-a3f3cd65-1285-4c12-b242-dcf1e370e95b.png)  

