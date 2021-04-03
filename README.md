# LiteDBPerformance

## 🎉 How to contribute
If you want to contribute with your testing environmente, please, fork the project, build the solution and launch the program directly from the file system.
The debug session with Visual Studio causes a lower perfomances result.
Copy and paste the results from the console directly in the Readme file.

## ✔ Tests
Tested on:
Surface Book 3 - i7, 32GB Ram

```
LiteDB: default - 5000 records
==============================
Insert         :  1193 ms -     4190 records/second
Bulk           :    77 ms -    64775 records/second
Update         :   360 ms -    13884 records/second
CreateIndex    :   201 ms -    24869 records/second
Query          :   565 ms -     8848 records/second
Delete         :  1744 ms -     2866 records/second
Drop           :    58 ms -    85846 records/second
File Size : 12088 kb

LiteDB: encrypted - 5000 records
================================
Insert         :   767 ms -     6514 records/second
Bulk           :    71 ms -    70117 records/second
Update         :   534 ms -     9357 records/second
CreateIndex    :   216 ms -    23087 records/second
Query          :   348 ms -    14340 records/second
Delete         :  1909 ms -     2618 records/second
Drop           :    58 ms -    85176 records/second
File Size : 11960 kb
```

---

Tested on:
Desktop Machine: Ryzen 3700X, 16GB Ram

```
LiteDB: default - 5000 records
==============================
Insert         :   734 ms -     6809 records/second
Bulk           :    58 ms -    85029 records/second
Update         :   348 ms -    14329 records/second
CreateIndex    :   186 ms -    26832 records/second
Query          :   367 ms -    13600 records/second
Delete         :   997 ms -     5014 records/second
Drop           :    36 ms -   138843 records/second
File Size : 11912 kb

LiteDB: encrypted - 5000 records
================================
Insert         :  4283 ms -     1167 records/second
Bulk           :    53 ms -    93968 records/second
Update         :  1456 ms -     3433 records/second
CreateIndex    :   427 ms -    11696 records/second
Query          :   320 ms -    15610 records/second
Delete         :  9465 ms -      528 records/second
Drop           :   436 ms -    11453 records/second
File Size : 11888 kb
```

---

Tested on:
MacBook Pro 2014: i7, 16GB Ram

```
LiteDB: default - 5000 records
==============================
Insert         :   870 ms -     5743 records/second
Bulk           :    66 ms -    75617 records/second
Update         :   346 ms -    14421 records/second
CreateIndex    :   222 ms -    22501 records/second
Query          :   412 ms -    12127 records/second
Delete         :  1216 ms -     4112 records/second
Drop           :    45 ms -   109437 records/second
File Size : 11960 kb

LiteDB: default - 5000 records
==============================
Insert         :   802 ms -     6228 records/second
Bulk           :   105 ms -    47601 records/second
Update         :   347 ms -    14375 records/second
CreateIndex    :   221 ms -    22614 records/second
Query          :   404 ms -    12363 records/second
Delete         :  1218 ms -     4104 records/second
Drop           :    42 ms -   116596 records/second
File Size : 12032 kb

LiteDB: encrypted - 5000 records
================================
Insert         :   587 ms -     8515 records/second
Bulk           :    63 ms -    78599 records/second
Update         :   378 ms -    13201 records/second
CreateIndex    :   172 ms -    28922 records/second
Query          :   321 ms -    15543 records/second
Delete         :   932 ms -     5364 records/second
Drop           :    27 ms -   179163 records/second
File Size : 12024 kb

SQLite: default - 5000 records
==============================
Insert         :  4764 ms -     1049 records/second
Bulk           :   752 ms -     6645 records/second
Update         :  4664 ms -     1072 records/second
CreateIndex    :    14 ms -   353965 records/second
Query          :   103 ms -    48221 records/second
Delete         :     3 ms -  1454969 records/second
Drop           :     3 ms -  1556808 records/second
File Size :  8848 kb

SQLite: encrypted - 5000 records
================================
Insert         :  5831 ms -      857 records/second
Bulk           :   776 ms -     6438 records/second
Update         :  5699 ms -      877 records/second
CreateIndex    :    66 ms -    74924 records/second
Query          :   145 ms -    34469 records/second
Delete         :   187 ms -    26706 records/second
Drop           :   177 ms -    28172 records/second
File Size :  9036 kb
```
