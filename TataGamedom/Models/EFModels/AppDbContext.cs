using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace TataGamedom.Models.EFModels
{
	public partial class AppDbContext : DbContext
	{
		public AppDbContext()
			: base("name=AppDbContext")
		{
		}

		public virtual DbSet<Announcement> Announcements { get; set; }
		public virtual DbSet<ApprovalStatusCode> ApprovalStatusCodes { get; set; }
		public virtual DbSet<BackendMember> BackendMembers { get; set; }
		public virtual DbSet<BackendMembersPermissionsCode> BackendMembersPermissionsCodes { get; set; }
		public virtual DbSet<BackendMembersRolePermission> BackendMembersRolePermissions { get; set; }
		public virtual DbSet<BackendMembersRolesCode> BackendMembersRolesCodes { get; set; }
		public virtual DbSet<Board> Boards { get; set; }
		public virtual DbSet<BoardsModerator> BoardsModerators { get; set; }
		public virtual DbSet<BoardsModeratorsApplication> BoardsModeratorsApplications { get; set; }
		public virtual DbSet<BucketLog> BucketLogs { get; set; }
		public virtual DbSet<Cart> Carts { get; set; }
		public virtual DbSet<Coupon> Coupons { get; set; }
		public virtual DbSet<CouponsProduct> CouponsProducts { get; set; }
		public virtual DbSet<DiscountTypeCode> DiscountTypeCodes { get; set; }
		public virtual DbSet<FAQ> FAQs { get; set; }
		public virtual DbSet<GameClassificationGame> GameClassificationGames { get; set; }
		public virtual DbSet<GameClassificationsCode> GameClassificationsCodes { get; set; }
		public virtual DbSet<GameComment> GameComments { get; set; }
		public virtual DbSet<GamePlatformsCode> GamePlatformsCodes { get; set; }
		public virtual DbSet<Game> Games { get; set; }
		public virtual DbSet<InventoryItem> InventoryItems { get; set; }
		public virtual DbSet<Issue> Issues { get; set; }
		public virtual DbSet<IssueStatusCode> IssueStatusCodes { get; set; }
		public virtual DbSet<IssueTypesCode> IssueTypesCodes { get; set; }
		public virtual DbSet<MemberProductView> MemberProductViews { get; set; }
		public virtual DbSet<Member> Members { get; set; }
		public virtual DbSet<MembersBoard> MembersBoards { get; set; }
		public virtual DbSet<News> News { get; set; }
		public virtual DbSet<NewsCategoryCode> NewsCategoryCodes { get; set; }
		public virtual DbSet<NewsComment> NewsComments { get; set; }
		public virtual DbSet<NewsletterLog> NewsletterLogs { get; set; }
		public virtual DbSet<Newsletter> Newsletters { get; set; }
		public virtual DbSet<NewsLike> NewsLikes { get; set; }
		public virtual DbSet<NewsView> NewsViews { get; set; }
		public virtual DbSet<OrderItemReturn> OrderItemReturns { get; set; }
		public virtual DbSet<OrderItem> OrderItems { get; set; }
		public virtual DbSet<OrderItemsCoupon> OrderItemsCoupons { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<OrderStatusCode> OrderStatusCodes { get; set; }
		public virtual DbSet<PaymentStatusCode> PaymentStatusCodes { get; set; }
		public virtual DbSet<PostCommentReport> PostCommentReports { get; set; }
		public virtual DbSet<PostComment> PostComments { get; set; }
		public virtual DbSet<PostCommentUpDownVote> PostCommentUpDownVotes { get; set; }
		public virtual DbSet<PostEditLog> PostEditLogs { get; set; }
		public virtual DbSet<PostReport> PostReports { get; set; }
		public virtual DbSet<Post> Posts { get; set; }
		public virtual DbSet<PostUpDownVote> PostUpDownVotes { get; set; }
		public virtual DbSet<ProductImage> ProductImages { get; set; }
		public virtual DbSet<Product> Products { get; set; }
		public virtual DbSet<ProductStatusCode> ProductStatusCodes { get; set; }
		public virtual DbSet<Reply> Replies { get; set; }
		public virtual DbSet<ShipmemtMethod> ShipmemtMethods { get; set; }
		public virtual DbSet<ShipmentStatusesCode> ShipmentStatusesCodes { get; set; }
		public virtual DbSet<StockInSheet> StockInSheets { get; set; }
		public virtual DbSet<StockInStatusCode> StockInStatusCodes { get; set; }
		public virtual DbSet<Supplier> Suppliers { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ApprovalStatusCode>()
				.HasMany(e => e.BoardsModeratorsApplications)
				.WithRequired(e => e.ApprovalStatusCode)
				.HasForeignKey(e => e.ApprovalStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMember>()
				.Property(e => e.Account)
				.IsUnicode(false);

			modelBuilder.Entity<BackendMember>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<BackendMember>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<BackendMember>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.BucketLogs)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.BackendMmemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Coupons)
				.WithRequired(e => e.BackendMember)
				.HasForeignKey(e => e.CreatedBackendMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Coupons1)
				.WithOptional(e => e.BackendMember1)
				.HasForeignKey(e => e.ModifiedBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.GameComments)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.DeleteBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Games)
				.WithRequired(e => e.BackendMember)
				.HasForeignKey(e => e.CreatedBackendMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Games1)
				.WithOptional(e => e.BackendMember1)
				.HasForeignKey(e => e.ModifiedBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.News)
				.WithRequired(e => e.BackendMember)
				.HasForeignKey(e => e.BackendMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.News1)
				.WithOptional(e => e.BackendMember1)
				.HasForeignKey(e => e.DeleteBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.NewsComments)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.DeleteBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.PostComments)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.DeleteBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.PostCommentReports)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.ReviewerBackenMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.PostReports)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.ReviewerBackenMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Posts)
				.WithOptional(e => e.BackendMember)
				.HasForeignKey(e => e.DeleteBackendMemberId);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.BackendMember)
				.HasForeignKey(e => e.CreatedBackendMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMember>()
				.HasMany(e => e.Products1)
				.WithOptional(e => e.BackendMember1)
				.HasForeignKey(e => e.ModifiedBackendMemberId);

			modelBuilder.Entity<BackendMembersPermissionsCode>()
				.HasMany(e => e.BackendMembersRolePermissions)
				.WithRequired(e => e.BackendMembersPermissionsCode)
				.HasForeignKey(e => e.BackendMemberPermissionId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMembersRolesCode>()
				.HasMany(e => e.BackendMembers)
				.WithRequired(e => e.BackendMembersRolesCode)
				.HasForeignKey(e => e.BackendMembersRoleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<BackendMembersRolesCode>()
				.HasMany(e => e.BackendMembersRolePermissions)
				.WithRequired(e => e.BackendMembersRolesCode)
				.HasForeignKey(e => e.BackendMembersRoleId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Board>()
				.Property(e => e.BoardHeaderCoverImg)
				.IsUnicode(false);

			modelBuilder.Entity<Board>()
				.HasMany(e => e.BoardsModerators)
				.WithRequired(e => e.Board)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Board>()
				.HasMany(e => e.BoardsModeratorsApplications)
				.WithRequired(e => e.Board)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Board>()
				.HasMany(e => e.BucketLogs)
				.WithRequired(e => e.Board)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Board>()
				.HasMany(e => e.MembersBoards)
				.WithRequired(e => e.Board)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Coupon>()
				.HasMany(e => e.CouponsProducts)
				.WithRequired(e => e.Coupon)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<DiscountTypeCode>()
				.HasMany(e => e.Coupons)
				.WithRequired(e => e.DiscountTypeCode)
				.HasForeignKey(e => e.DiscountTypeId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<GameClassificationsCode>()
				.HasMany(e => e.GameClassificationGames)
				.WithRequired(e => e.GameClassificationsCode)
				.HasForeignKey(e => e.GameClassificationId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<GamePlatformsCode>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<GamePlatformsCode>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.GamePlatformsCode)
				.HasForeignKey(e => e.GamePlatformId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Game>()
				.HasMany(e => e.GameClassificationGames)
				.WithRequired(e => e.Game)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Game>()
				.HasMany(e => e.GameComments)
				.WithRequired(e => e.Game)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Game>()
				.HasMany(e => e.News)
				.WithOptional(e => e.Game)
				.HasForeignKey(e => e.GamesId);

			modelBuilder.Entity<InventoryItem>()
				.Property(e => e.Cost)
				.HasPrecision(8, 0);

			modelBuilder.Entity<InventoryItem>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.InventoryItem)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<IssueStatusCode>()
				.HasMany(e => e.Issues)
				.WithOptional(e => e.IssueStatusCode)
				.HasForeignKey(e => e.Status);

			modelBuilder.Entity<IssueTypesCode>()
				.HasMany(e => e.FAQs)
				.WithOptional(e => e.IssueTypesCode)
				.HasForeignKey(e => e.IssueTypeId);

			modelBuilder.Entity<IssueTypesCode>()
				.HasMany(e => e.Issues)
				.WithOptional(e => e.IssueTypesCode)
				.HasForeignKey(e => e.IssueTypeId);

			modelBuilder.Entity<Member>()
				.Property(e => e.Account)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Email)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.Phone)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.Property(e => e.ConfirmCode)
				.IsUnicode(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.BoardsModerators)
				.WithRequired(e => e.Member)
				.HasForeignKey(e => e.ModeratorMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.BoardsModeratorsApplications)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.BucketLogs)
				.WithRequired(e => e.Member)
				.HasForeignKey(e => e.BucketMemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.BucketLogs1)
				.WithOptional(e => e.Member1)
				.HasForeignKey(e => e.ModeratorMemberId);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.GameComments)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.MemberProductViews)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.MembersBoards)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.NewsComments)
				.WithOptional(e => e.Member)
				.HasForeignKey(e => e.DeleteMemberId);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.NewsComments1)
				.WithRequired(e => e.Member1)
				.HasForeignKey(e => e.MemberID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.NewsletterLogs)
				.WithRequired(e => e.Member)
				.HasForeignKey(e => e.AddresseeMemberID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.NewsLikes)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.NewsViews)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostComments)
				.WithOptional(e => e.Member)
				.HasForeignKey(e => e.DeleteMemberId);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostComments1)
				.WithRequired(e => e.Member1)
				.HasForeignKey(e => e.MemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostCommentUpDownVotes)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostCommentReports)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostReports)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Posts)
				.WithOptional(e => e.Member)
				.HasForeignKey(e => e.DeleteMemberId);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.Posts1)
				.WithRequired(e => e.Member1)
				.HasForeignKey(e => e.MemberId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Member>()
				.HasMany(e => e.PostUpDownVotes)
				.WithRequired(e => e.Member)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<News>()
				.HasMany(e => e.NewsComments)
				.WithRequired(e => e.News)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<News>()
				.HasMany(e => e.NewsLikes)
				.WithRequired(e => e.News)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<News>()
				.HasMany(e => e.NewsViews)
				.WithRequired(e => e.News)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<NewsCategoryCode>()
				.HasMany(e => e.News)
				.WithRequired(e => e.NewsCategoryCode)
				.HasForeignKey(e => e.NewsCategoryId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<NewsletterLog>()
				.Property(e => e.AddresseeMemberEmail)
				.IsUnicode(false);

			modelBuilder.Entity<OrderItem>()
				.Property(e => e.ProductPrice)
				.HasPrecision(8, 0);

			modelBuilder.Entity<OrderItem>()
				.HasMany(e => e.OrderItemReturns)
				.WithRequired(e => e.OrderItem)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderItem>()
				.HasMany(e => e.OrderItemsCoupons)
				.WithRequired(e => e.OrderItem)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Order)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<OrderStatusCode>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.OrderStatusCode)
				.HasForeignKey(e => e.OrderStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PaymentStatusCode>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.PaymentStatusCode)
				.HasForeignKey(e => e.PaymentStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PostComment>()
				.HasMany(e => e.PostCommentReports)
				.WithRequired(e => e.PostComment)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<PostComment>()
				.HasMany(e => e.PostComments1)
				.WithOptional(e => e.PostComment1)
				.HasForeignKey(e => e.ParentId);

			modelBuilder.Entity<PostComment>()
				.HasMany(e => e.PostCommentUpDownVotes)
				.WithRequired(e => e.PostComment)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Post>()
				.HasMany(e => e.PostComments)
				.WithRequired(e => e.Post)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Post>()
				.HasMany(e => e.PostEditLogs)
				.WithRequired(e => e.Post)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Post>()
				.HasMany(e => e.PostReports)
				.WithRequired(e => e.Post)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Post>()
				.HasMany(e => e.PostUpDownVotes)
				.WithRequired(e => e.Post)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.Carts)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.CouponsProducts)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.InventoryItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.MemberProductViews)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.OrderItems)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Product>()
				.HasMany(e => e.ProductImages)
				.WithRequired(e => e.Product)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ProductStatusCode>()
				.HasMany(e => e.Products)
				.WithRequired(e => e.ProductStatusCode)
				.HasForeignKey(e => e.ProductStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ShipmemtMethod>()
				.Property(e => e.Cost)
				.HasPrecision(8, 0);

			modelBuilder.Entity<ShipmemtMethod>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.ShipmemtMethod)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<ShipmentStatusesCode>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.ShipmentStatusesCode)
				.HasForeignKey(e => e.ShipmentStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<StockInSheet>()
				.HasMany(e => e.InventoryItems)
				.WithRequired(e => e.StockInSheet)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<StockInStatusCode>()
				.HasMany(e => e.StockInSheets)
				.WithRequired(e => e.StockInStatusCode)
				.HasForeignKey(e => e.StockInStatusId)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Supplier>()
				.HasMany(e => e.StockInSheets)
				.WithRequired(e => e.Supplier)
				.WillCascadeOnDelete(false);
		}
	}
}
