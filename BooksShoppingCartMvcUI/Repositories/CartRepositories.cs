using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration.UserSecrets;
using System.Threading.Tasks;

namespace BooksShoppingCartMvcUI.Repositories
{

    public class CartRepositories :ICartRepositories
    {
        private readonly ApplicationDbContext _db;
        private readonly HttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;
        public CartRepositories(ApplicationDbContext db,HttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
                                                                     
        {
            _db=db;
            _httpContextAccessor=httpContextAccessor;
            _userManager=userManager;
        }
       public async Task<bool> AddCart(int bookId, int qty)
        {
            using var transaction = _db.Database.BeginTransaction();
            try
            {

                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    cart =new ShoppingCart
                    {
                        UserId = userId,
                    };
                    _db.ShoppingCarts.Add(cart);
                }
                _db.SaveChanges();
                //Now Add cartDetails
                var cartItem=_db.CartDetailses.FirstOrDefault(X=>X.ShoppingCartId==cart.Id && X.BookId==bookId);
                if(cartItem is not null)
                {
                    cartItem.Quantity +=qty;
                }
                else
                {
                    cartItem=new CartDetails
                    {
                        Quantity=qty,
                        BookId=bookId,
                        ShoppingCartId=cart.Id
                    };
                    _db.CartDetailses.Add(cartItem);

                }
                _db.SaveChanges();
                 transaction.Commit();
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
       public async Task<bool> RemoveCart(int bookId)
        {
            //using var transaction = _db.Database.BeginTransaction();
            try
            {

                string userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                {
                    return false;
                }
                var cart = await GetCart(userId);
                if (cart is null)
                {
                    return false;
                }
               
                //Now Add cartDetails
                var cartItem=_db.CartDetailses.FirstOrDefault(X=>X.ShoppingCartId==cart.Id && X.BookId==bookId);
                if(cartItem is  null)
                {
                   return false;
                }
                else if(cartItem.Quantity==1)
                {
                   
                    _db.CartDetailses.Remove(cartItem);
                }
                else
                {
                    cartItem.Quantity=cartItem.Quantity-1;
                }
                _db.SaveChanges();
                 //transaction.Commit();
                return true;
                
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public async Task<IEnumerable<ShoppingCart>> GetUserCart()
        {
            var userId=GetUserId();
            if (userId==null)
                throw new Exception("Invalid userId");
            var shoppingCart = await _db.ShoppingCarts
                .Include(a => a.CartDetails)
                .ThenInclude(a => a.Book)
                .ThenInclude(a => a.Genre)
                .Where(a => a.UserId==userId).ToListAsync();
            return shoppingCart;
                                
            

        }
        private async Task<ShoppingCart> GetCart(string userId)
        {
            var cart=await _db.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId= _userManager.GetUserId(principal);
            return userId;
        }
    }
}
