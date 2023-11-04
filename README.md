# re-do-music

The ReDoMusic project is being developed as part of the Yetgen & Akbank .NET boot camp.

The developers of this project are Team 3, which was formed within the scope of the boot camp. The team members and their GitHub profiles are as follows:

- [`Celal Karahan`](https://github.com/k-celal)
- [`Hatice Deveci`](https://github.com/hatice-dvc)
- [`Bahar Erol`](https://github.com/baharerol)
- [`Zubeyde Yasa`](https://github.com/zubeydeyasa)


## PROJE AÇIKLAMASI
  - ReDoMusic projesi, enstrüman alım - satımı yapılabilen bir alışveriş web sitesidir. Siteye kullanıcı olarak girip ürünleri görüntüleyebilir ve sepete ekleyerek satın alabilirsiniz ya da site yöneticisi olarak sitede değişiklikler yapabilirsiniz.
     

## PROJEYE EKLENEN ÖZELLİKLER

## Kullanıcıların Siteye Kayıt Olabilmesi ve Giriş Yapabilmesi
- Kullanıcı isim - soyisim ve mail adresi ile siteye kayıt olabilir, kullanıcı adı ve şifresi ile siteye giriş yapabilir.
## Yönetici Özelliği
- Site yöneticisi de kendi kullanıcı adı ve şifresiyle siteye giriş yapabilir.
## Kullanıcıların Sitedeki Enstrümanları Görüntüleyebilmesi
- Kullanıcılar giriş yaptıktan sonra sitedeki bütün enstrümanları ve bunların özelliklerini görüntüleyebilir.
## Sepete Ekleme Özelliği
- Kullanıcılar satın almak istedikleri ürünleri kolaylıkla kendi sepetlerine ekleyebilir.
## Sipariş Oluşturma Özelliği
   - Müşteriler sepetlerine ekledikleri ürünleri istedikleri ödeme yöntemini seçerek satın alabilir.
## Yöneticinin Siparişleri Görüntüleyebilmesi
   - Site yöneticisi verilen siparişleri; sipariş içindeki ürünleri, sipariş tarihini ve ödeme yöntemini liste halinde görüntüleyebilir.
## İletişim sayfası
   - Kullanıcının, web sitesinin yöneticisiyle iletişim kurmasını sağlar.
   

## PROJENİN TEKNİK DETAYI

## Core/Entities
- AppRole adında bir entity oluşturduk. Bu entity IdentityRole kütüphanesinden kalıtım alıyor ve rol atama (kullanıcı veya yönetici rolü) işlevini gerçekleştiriyor.
- AppUser adında bir entity oluşturduk. Bu entity IdentityUser kütüphanesinden kalıtım alıyor ve uygulamayı birden fazla kullancının kullanabilmesine olanak sağlıyor.
- Basket adında bir entity oluşturduk. Bu entity EntityBase'den kalıtım alıyor. Kullanıcıların sepete eklediği ürünleri BasketItem adında bir listede tutuyor. BasketItem class'ı da EntityBase'den kalıtım alıyor. İçerisinde Instrument ve Quantity property'lerini tutuyor. 
- Order adında bir entity oluşturduk. Bu entity EntityBase'den kalıtım alıyor. İçerisinde Customer, ShippingAddress, PaymentMethod, OrderDate, Status ve  OrderItems property'lerini tutuyor. Customer property'sini AppUser tipinde oluşturduk. PaymentMethod ve Status property'lerini enum yapısıyla oluşturduk. OrderItems ise Basket tipinde tutuluyor.
- ContactMessage adında bir entity oluşturduk. İçerisinde Id, Name, Email, Message ve CreatedAt property'leri var.

## Core/Enums
- OrderStatus adında bir enum oluşturduk. Sipariş durumlarını(sipariş beklemede/sevk edildi/teslim edildi/iptal edildi) tutuyor.
- Payment adında bir enum oluşturduk. Ödeme yöntemlerini (nakti/kredi kartı/banka kartı/havale/diğer) tutuyor.

## Infrastructure/Contexts
- DbCotext oluşturduk. Order, Basket ve BasketItem adında, DbSet tipinde veritabanı tabloları oluşturduk. Migration atarak veritabanı bağlantısı kurduk ve veritabanımızı verilerle güncelledik.

## Presentation/Areas/Admin/Controllers
- HomeController oluşturduk. Bu controller'a sadece "admin" rolüne sahip kullanıcılar erişebilir. Siteye kayıtlı kullanıcıları listeleme, iletişim mesajlarını listeleme ve silme yöntemlerini içeriyor.
- RolesController oluşturduk. Kullanıcıların rolleri ile ilgili işlemleri gerçekleştiriyor. Rol oluşturma, rol güncelleme ve rol atama metotlarını içeriyor.

## Presentation/Areas/Admin/Models
- AssignRoleToUserViewModel'ı oluşturduk. İçerisinde Id, Name ve bool tipinde Exist property'leri var. Kullanıcılara user rolü atanmasını sağlıyor.
- RoleCreateViewModel'ı oluşturduk. Burada kullanıcı ya da yönetici rolü atama işlemi yapılıyor.
- RoleUpdateViewModel'ı oluşturduk. Var olan rolü güncelleme işlemini yapıyor.
- RoleViewModel'ı oluşturduk. Bir rolün temel bilgilerini taşımak için kullanılıyor. İçerisinde Id ve Name adında iki property tanımladık.
- UserViewModel'ı oluşturduk. Kullanıcının temel bilgilerini ve kullanıcının sahip olduğu rolleri gösteriyor. Id, Name, Email ve string dizisi tipinde Roles property'lerini içeriyor.

## Presentation/Areas/Admin/Views
- Home View'ını oluşturduk. Index.cshtml, UserList.cshtml ve ContactMessage.cshtml dosyalarını içeriyor. Index.cshtml'de admin paneli sayfasını, UserList.cshtml'de üye listesi sayfasını, ContactMessage.cshtml'de ise iletişim sayfasının görünümünü oluşturduk. 
- Roles View'ını oluşturduk. AssignRoleToUser.cshtml, Index.cshtml, RoleCreate.cshtml ve RoleUpdate.cshtml dosyalarını içeriyor. ViewModel'lerini de yazdığımız bu sayfaların görünüm kısmını da oluşturmuş olduk. Index.cshtml dosyasında da rol listesinin görüntülenmesini sağladık.
- Shared View'ın içindeki LayOut.cshtml sayfasında web sitesinin genel görünümünü oluşturduk. Error.cshtml'de ise bir hata durumunda kullanıcın hata mesajı görmesini sağladık.

## Presentation/Controllers/Home
- Home Controller oluşturduk. Kullanıcının kayıt olması ve giriş yapması işlemlerini yönetiyor.
- Member Controller oluşturduk. Yetkilendirme işlemlerini yönetme, kullanıcı oturumunu sonlandırma ve yetkilendirme hatası durumunda ret mesajı gösterme işlmelerini gerçekleştiriyor.

## Presentation/Controllers
- Basket Controller oluşturduk. Kullanıcının sepet işlemlerini yönetiyor. Sepete ekleme, sepeti güncelleme ve sepetten ürün silme işlemlerini yapmamızı sağlıyor.
- Order Controller oluşturduk. Müşteri siparişlerini yönetiyor. Başarıyla tamamlanan sipariş işlemleri için OrderSuccess metodunu, yeni sipariş oluşturma ve sipariş silme metotlarını içeriyor.

## Presentation/Extensions
- ModelStateExtensions uzantısını kullandık. Bu, "ModelStateDictionary" sınıfını genişleten bir dizi yardımcı yöntem içeren bir C# uzantı sınıfıdır. Bu yardımcı yöntemler, ModelState nesnesine birden fazla hata eklemeyi kolaylaştırmak için tasarlanmıştır.Bunu kullanıcı girişi doğrulama ve işlem sonuçlarını görsel olarak sunmak için kullandık.
- SessionExtensions uzzantısını kullandık.Bu, session verilerini işlemek için kullanılabilecek genişletilmiş yöntemleri içeren bir C# sınıfını tanımlar. 
Projemizde; kullanıcı özelleştirmeleri, sepet bilgileri, kullanıcı oturumlarının sürdürülmesi gibi durumlar için kullandık.

## Presentation/Models
- Hata sayfalarını görüntülemek için ErrorViewModel oluşturduk. 

## Presentation/Properties
- Eklediğimiz "launchSettings.json" dosyası, geliştirme sırasında uygulamanın nasıl çalıştırılacağını, hangi URL adreslerinin kullanılacağını ve geliştirme ortamını ayarlamak için kullanılır. Bu ayarlar, farklı profiller aracılığıyla uygulamanın farklı koşullar altında nasıl davranacağını belirlememize yardımcı oluyor.

## Presentation/TagHelpers
- UserRoleNamesTagHelper, kullanıcının rollerini görsel olarak temsil eden HTML etiketlerini oluşturur. Bu, özellikle kullanıcı profilleri veya kullanıcı ayrıntıları sayfalarında kullanıcı rollerini göstermek içindir.

## Presentation/ViewModels
- BasketAndOrderViewModel'ı oluşturduk. Bu, kullanıcıların alışveriş sepetlerini ve sipariş bilgilerini tek bir modele toplamak için kullanılır. Özellikle kullanıcıların alışveriş yaparken sepet içeriği ile ödeme ve teslimat bilgilerini aynı formda doldurabilmesini sağlar.
- İleitşim sayfası, kayıt olma ve giriş yapma view model'lerini de oluşturduk.

## Presentation/Views
Sepet, Home(contact, privacy, sign-up ve sign-in), Member(erişimin engellendiği bilgisi), ve Order görünümlerini oluşturduk.


## GÖREV DAĞILIMI

## Celal Karahan
Bu projede admin ve kullanıcı için ayrı ayrı kayıt olma ve giriş yapma özelliklerini eklemekle görevliydim.
 - Identity kütüphanesi kullanarak "giriş yap", "kayıt ol" gibi özellikleri veri tabanına bağlı bir şekilde ekledim. 
 - Admin özellikleri ve adminin kullanabileceği paneli tasarladım. Bunlar için rolleri ayarladım.
 - Instrument entity oluşturup InstrumentController’ı yazdım. 
 - BasketController'ın son halini yazdım ve BasketView ekledim.
 - "Giriş yap" "kayıt ol" gibi özellikler için birçok controller ve model oluşturdum. 
 - Tüm bu işlemleri görüntüleyebilmemizi sağlayan view’ları ekledim. 

## Hatice Deveci
Bu projede sipariş özelliğini oluşturmakla görevliydim.
- Order entity oluşturdum. Bu entity içerisinde Customer, ShippingAddress, PaymentMethod, OrderDate, Status ve  OrderItems property'leri bulunuyor.
- Ödeme türünü ve sipariş durumunu kullanıcıların seçimini kolaylıkla yapabilmesi adına enum olarak oluşturdum.
- DbContext içerisinde DbSet ile Orders tablosunu oluşturdum ve sipariş verilerini bu tablo üzerinde güncelledim.
- OrderController yazdım. OrderController, içerisinde bulunan metotlar sayesinde başarılı bir siparişin tamamlandığı sayfayı gösterme, sipariş oluşturma ve bir siparişi silme işlemlerini gerçekleştiriyor.
- Sipariş ekleme sayfasını ve eklenen siparişlerin bilgilerini görüntüleyebileceğimiz sayfaları oluşturdum. 

## Bahar Erol
Bu projede kullanıcının sepet işlemlerini eklemekle görevliydim.
- Basket entity oluşturdum.
- DbContext içerisinde DbSet ile Baskets tablosunu oluşturdum ve sepet verilerini bu tablo üzerinde güncelledim.
- BasketController'ın ilk halini yazdım.

## Zübeyde Yaşa
Bu projede iletişim sayfası özelliği eklemekle görevliydim.
- Kullanıcının site yöneticilerine mesaj gönderebilmesi özelliğini yazdım ve görünümünü oluşturdum.

## YAŞADIĞIMIZ PROBLEMLER
Veritabanı tarafında,
- Tablo içinde tablo tutulması (sınıfın içerisinde farklı bir sınıfı kullanmak),
- Session yapısının veritabanı tarafından kontrol edilememesi (veritabanı, session'da gerçekleşen sepet olaylarını kontrol edemiyor ve sepeti sipariş kısmında kullandığımız için hataya sebep oluyordu.)
gibi birçok sorunla karşılaştık.

- Siparişin tek bir kullanıcıya ait olmasını sağlamada, 
- Ödeme yöntemi ve sipariş durumunu enum yaptığımızda, web sitesi üzerinde bunların gösterimini yapmakta,
- Alışveriş sepetlerinin farklı farklı kullacılara ait olmasını sağlamada,
- Uygulamanın farklı kullanıcılar tarafından kullanılmasını sağlamakta zorlandık.

Takım içinde yardımlaşarak, internet üzerinde izlediğimiz videolarla, yaptığımız araştırmalarla, eğitmenlerimizden yardım alarak bu sorunları çözdük.













