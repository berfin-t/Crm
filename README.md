# CRM (Customer Relationship Management)  - ABP Framework & Blazor

Bu proje, **ABP Framework** ile geliştirilmiş bir **müşteri ilişkileri yönetimi** uygulamasıdır. **Blazor** tabanlı bir arayüze sahiptir ve ABP Framework'ün  modüler yapısını kullanmaktadır.

## Kurulum

Projenizi yerel ortamda çalıştırmak için aşağıdaki adımları izleyin.

### Gerekli Araçlar
Aşağıdaki araçlar yüklü olmalı
- **.NET SDK 9.0** veya üzeri

### Adımlar

#### 1. Projeyi Klonlayın
```sh
git clone [https://github.com/berfin-t/Crm.git](https://github.com/berfin-t/Crm.git)
cd Crm
```

#### 2.Docker network oluşturun

```sh
docker network create crm-backend
```

#### 3.Dockerı çalıştırın

```sh
cd docker compose up -d 
```

#### 4.Migration için

```sh
docker compose -f migrator-compose.yml run --rm -d migrator 
```

#### 6. Uygulamayı Açın
Tarayıcınızda aşağıdaki URL'lere giderek projeyi görüntüleyebilirsiniz:
- **Blazor UI:** [https://localhost:44376](https://localhost:44376)

---

## Kullanılan Teknolojiler

- **ABP Framework**
- **Blazor(Blazorise)**
- **Entity Framework Core**

