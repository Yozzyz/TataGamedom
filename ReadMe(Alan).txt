Working On
[v] Add InventoryVM,InventoryItemVM,InventoryItemDto
[] 庫存Index  => Service Search()


To Do
[] 寄信  庫存 detail => 進貨單CRUD
[] 進貨推薦清單
[] Create Order 要檢查Member id是否存在

[]退貨單
[]Edit Order 變更物流狀態時寄信


[]使能選擇輸入欄位要篩什麼
[]使能選擇
	訂單狀態 &  訂單編號 & 顯示幾筆
	訂單狀態 OR 訂單編號(輸入欄位) & 顯示幾筆

Completed
06/26
[v]Create EFModel
[v]Add Controller & View & LayOut2
[v]Add OrdersIndexVM, Add OrdersIndex Controller , Index View
[v]Orders Index()改三層式, 改的順序:DTO => IRepo => Repo => Services => Controller
note: 用Dapper就不需要先轉Entity轉Dto了? 因為他是直接對資料庫做操作? XX  簡單的操作或許可以，但如果變得複雜，先轉成Dto還是好操作一些

06/27
[v]新增Service,Repo併為一個
[v]訂單狀態 & [v]訂單編號(輸入欄位)
[v]SelectList
[v]單元測試

6/28
[V]排序功能 Index CreatedAt Status*3 total  => ApplySort() 傳入參數放IEnumarable (已Debug)
[v]排序功能結合篩選功能, unitTest => TestGetUrl

06/29
[v]排序功能 Debug
[v]分頁功能
步驟
//View 呼叫連結
//取得Service.Search(criteria, sortInfo).Count() 作為TotalRecords 
// 設定PageInfo 
	property
	 => TotalRecords 篩選後資料筆數, PageSize 每頁幾筆 , PageNumber 點擊第幾頁
	       Pages 顯示幾頁(btn) 
	       PageItemCount(預設頁數?)
	       PageItemPrevNumber首頁btn對應頁數
	       PageItemNextNumber結尾btn對應頁數
	       PageBarItemCount(預設button數)

06/30
[v]Add Order Create() VM,Dto
[v]Order Create IRepo repo Service []功能 => Index編碼
[v]Order Create View Controller 
[v] Orders Create 修改DataAnnotations&下拉清單
[v] debug Index Sql
[v] Add Order Info()
[v] Order Edit Delete