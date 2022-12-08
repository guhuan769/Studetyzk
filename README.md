DI包 Microsoft.Extensions.DependencyInjection
GIT 指定分支拉去代码语法 git clone -b gh https://github.com/guhuan769/Studetyzk.git  
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

