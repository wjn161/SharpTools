SharpTools Changelog
=========
参考了[Semantic Versioning](http://semver.org/) 对版本管理的描述，我们打算使用如下的版本号模板作为SharpTools的release版本号

1. `<major>.<minor>.<patch>-alpha`
2. `<major>.<minor>.<patch>-beta`
3. `<major>.<minor>.<patch>-rc`
4. `<major>.<minor>.<patch>`

修改版本号的各个部分将遵循如下的规范

* 新版本新增的内容如果破坏了向后兼容性，则会修改major部分 
* 新版本新增的内容如果不会破坏向后兼容性，则会修改minor部分
* 修复bug或者新增了其他非核心内容，则会修改patch部分

**以上描述的详细内容可以参考[semver.org](http://semver.org/)**

综合以上描述，我们可以得到的第一个版本号将是 `0.0.1-alpha`

---
### 0.0.1 2013-11-17

* A New Day Has Come
* 