## 目录 
 [异步方法解析](#异步方法解析) 
# 异步方法解析
![image](https://user-images.githubusercontent.com/46043439/205428691-615c2049-473c-4ee8-8f58-addd9d30b592.png)  
![image](https://user-images.githubusercontent.com/46043439/205429178-18e8bfc5-e6f2-4499-a813-25b21ad8171b.png)  
如:像WinFrom中Button按钮事件 由于委托的固定返回值 所以不支持异步 那么又非要用异步应该怎么处理呢 只需要在异步方法.Result()读值即可解决
![image](https://user-images.githubusercontent.com/46043439/205429345-c59af2ec-719e-4f2f-8184-5484340f8edf.png)  
不支持异步 就可以手动的拿异步方法的值
![image](https://user-images.githubusercontent.com/46043439/205429471-6df262f6-4dbf-46aa-988e-0a00f3f8fde1.png)
那么如果我想写入值的话可以用wait()
![image](https://user-images.githubusercontent.com/46043439/205429529-3c3a124c-113e-4979-b10d-f46a3ab40edd.png)


