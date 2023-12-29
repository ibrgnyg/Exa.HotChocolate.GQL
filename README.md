# Exa 

# What is this Hot Chocoloate GraphQL ? 

![grafic](https://chillicream.com/static/cfd2ddde71f95ed876541f87c15b2a08/f97d7/platform.png "grafic")

Net core Hot Cholocate GraphQL (basic example)

- [Hot Chocolate  (documentation) ](https://chillicream.com/docs/hotchocolate/v13)
- [Defining a schemas  (documentation) ](https://chillicream.com/docs/hotchocolate/v13/defining-a-schema)
- Queries & Mutations

## Introduction (Enum Types Defining a schema)
```csharp
public class EnumResultType : EnumType<QLResultType>
{
    protected override void Configure(IEnumTypeDescriptor<QLResultType> descriptor)
    {
        descriptor
            .Value(QLResultType.Success)
            .Name("success");

        descriptor
            .Value(QLResultType.Error)
            .Name("error");

        descriptor
            .Value(QLResultType.Empty)
            .Name("empty");

        descriptor
            .Value(QLResultType.NotFound)
            .Name("notfound");

        descriptor
            .Value(QLResultType.Exception)
            .Name("exception");

        descriptor
            .Value(QLResultType.Duplicate)
            .Name("duplicate");
    }
}
```

## Introduction (Queries)

### products
- Result Type: `DTOPaginationOfProduct!`

### product
- Result Type: `Product!`

### categories
- Result Type: `DTOPaginationOfCategory!`

### category
- Result Type: `Category!`


# Query: Products
```json
query Products {
  products(activePage: 1, pageSize: 10) {
    activePage
    pageSize
    totalCount
    totalPageCount
    data {
      productName
      productDescription
      price
      categoryId
      tags
      id
      createDate
      updateDate
    }
  }
}
```
# Query: Result
```json
{
    "data": {
        "products": {
            "activePage": 1,
            "pageSize": 10,
            "totalCount": 1,
            "totalPageCount": 1,
            "data": [
                {
                    "productName": "ASUS GEFORCE RTX 4070 TI TUF-RTX4070TI-O12G-GAMING 12 GB GDDR6X",
                    "productDescription": "ASUS TUF Gaming GeForce RTX 4070 Ti 12GB GDDR6X OC Edition, DLSS 3",
                    "price": 7.999,
                    "categoryId": "Nvidia 4000 Series",
                    "tags": [
                        "Nvidia",
                        "4070ti",
                        "4070"
                    ],
                    "id": "faf50d59-76cd-4ffe-847e-f86535dcc786",
                    "createDate": "2023-12-28T23:49:14.124Z",
                    "updateDate": "0001-01-01T00:00:00.000Z"
                }
            ]
        }
    }
}
```
# Query: Product
```json
query Product {
    product(id: "faf50d59-76cd-4ffe-847e-f86535dcc786") {
        productName
        productDescription
        price
        categoryId
        tags
        id
        createDate
        updateDate
    }
}
```
# Query: Result
```json
{
    "data": {
        "product": {
            "productName": "ASUS GEFORCE RTX 4070 TI TUF-RTX4070TI-O12G-GAMING 12 GB GDDR6X",
            "productDescription": "ASUS TUF Gaming GeForce RTX 4070 Ti 12GB GDDR6X OC Edition, DLSS 3",
            "price": 7.999,
            "categoryId": "7e50e679-781a-4596-ba01-43f476b0283c",
            "tags": [
                "Nvidia",
                "4070ti",
                "4070"
            ],
            "id": "faf50d59-76cd-4ffe-847e-f86535dcc786",
            "createDate": "2023-12-28T23:49:14.124Z",
            "updateDate": "0001-01-01T00:00:00.000Z"
        }
    }
}
```
# Query: Categories
```json
query Categories {
    categories(activePage: 1, pageSize: 10) {
        activePage
        pageSize
        totalCount
        totalPageCount
        data {
            categoryName
            id
            createDate
            updateDate
        }
    }
}

```
# Query: Result
```json
{
    "data": {
        "categories": {
            "activePage": 1,
            "pageSize": 10,
            "totalCount": 1,
            "totalPageCount": 1,
            "data": [
                {
                    "categoryName": "Nvidia 4000 Series",
                    "id": "7e50e679-781a-4596-ba01-43f476b0283c",
                    "createDate": "2023-12-28T23:43:23.859Z",
                    "updateDate": "0001-01-01T00:00:00.000Z"
                }
            ]
        }
    }
}
```
# Query: Category
```json
query Category {
    category(id: "7e50e679-781a-4596-ba01-43f476b0283c") {
        categoryName
        id
        createDate
        updateDate
    }
}
```
# Query: Result
```json
{
    "data": {
        "category": {
            "categoryName": "Nvidia 4000 Series",
            "id": "7e50e679-781a-4596-ba01-43f476b0283c",
            "createDate": "2023-12-28T23:43:23.859Z",
            "updateDate": "0001-01-01T00:00:00.000Z"
        }
    }
}
```


## Introduction (Mutations)

## Mutation

### addProduct
- **Result Type:** `QLResult!`

### updateProduct
- **Result Type:** `QLResult!`

### deleteProduct
- **Result Type:** `QLResult!`

### addCategory
- **Result Type:** `QLResult!`

### updateCategory
- **Result Type:** `QLResult!`

### deleteCategory
- **Result Type:** `QLResult!`


# Mutation: AddProduct 
```json
mutation AddProduct {
    addProduct(
        model: {
            productName: "ASUS GEFORCE RTX 4070 TI TUF-RTX4070TI-O12G-GAMING 12 GB GDDR6X"
            productDescription: "ASUS TUF Gaming GeForce RTX 4070 Ti 12GB GDDR6X OC Edition, DLSS 3"
            categoryId: "7e50e679-781a-4596-ba01-43f476b0283c"
            tags: ["Nvidia", "4070ti", "4070"]
            images: [
                "https://m.media-amazon.com/images/I/81hNpQEwd6L._AC_SL1500_.jpg"
                "https://m.media-amazon.com/images/I/51MG174Ci6L._AC_.jpg"
                "https://m.media-amazon.com/images/I/51PytLVTZGL._AC_.jpg"
                "https://m.media-amazon.com/images/I/514qiXxd8dL._AC_.jpg"
                "https://m.media-amazon.com/images/I/51nrfLG8SjL._AC_.jpg"
            ]
            price: 7.999
        }
    ) {
        message
        isError
        resultType
    }
}
```

# Mutation: AddProduct Result
```json
{
    "data": {
        "addProduct": {
            "message": "succes_mes",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: UpdateProduct 
```json
mutation UpdateProduct {
    updateProduct(
        model: {
            id: "29b39a0a-d1ba-4d37-b18c-d479a65302a4"
            productName: "ASUS GEFORCE RTX 4070 TI TUF-RTX4070TI-O12G-GAMING 12 GB GDDR6X"
            productDescription: "ASUS TUF Gaming GeForce RTX 4070 Ti 12GB GDDR6X OC Edition, DLSS 3"
            categoryId: "7e50e679-781a-4596-ba01-43f476b0283c"
            tags: ["Nvidia", "4070ti", "4070"]
            images: [
                "https://m.media-amazon.com/images/I/81hNpQEwd6L._AC_SL1500_.jpg"
                "https://m.media-amazon.com/images/I/51MG174Ci6L._AC_.jpg"
                "https://m.media-amazon.com/images/I/51PytLVTZGL._AC_.jpg"
                "https://m.media-amazon.com/images/I/514qiXxd8dL._AC_.jpg"
                "https://m.media-amazon.com/images/I/51nrfLG8SjL._AC_.jpg"
            ]
            price: 7.999
        }
    ) {
        message
        isError
        resultType
    }
}
```

# Mutation: UpdateProduct Result
```json
{
    "data": {
        "updateProduct": {
            "message": "succes_mes",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: DeleteProduct 
```json
mutation DeleteProduct {
    deleteProduct(id: "29b39a0a-d1ba-4d37-b18c-d479a65302a4") {
        message
        isError
        resultType
    }
}
```

# Mutation: DeleteProduct Result
```json
{
    "data": {
        "deleteProduct": {
            "message": "",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: AddCategory 
```json
mutation AddCategory {
    addCategory(model: { categoryName: "Phones" }) {
        message
        isError
        resultType
    }
}
```

# Mutation: AddCategory Result
```json
{
    "data": {
        "addCategory": {
            "message": "succes_mes",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: UpdateCategory 
```json
mutation UpdateCategory {
    updateCategory(model: { id: "289r50e679-781a-4596-ba01-43f476b0-5843c", categoryName: "Phone" }) {
        message
        isError
        resultType
    }
}
```
# Mutation: UpdateCategory Result
```json
{
    "data": {
        "updateCategory": {
            "message": "succes_mes",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: DeleteCategory 
```json
    mutation DeleteCategory {
        deleteCategory(id: "1") {
            message
            isError
            resultType
        }
    }
```
# Mutation: UpdateCategory Result
```json
{
    "data": {
        "deleteCategory": {
            "message": "",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

# Mutation: DeleteCategory 
```json
mutation DeleteCategory {
    deleteCategory(id: "29b39a0a-d1ba-4d37-b18c-d479a65302a4") {
        message
        isError
        resultType
    }
}
```

# Mutation: DeleteCategory Result
```json
{
    "data": {
        "deleteCategory": {
            "message": "",
            "isError": false,
            "resultType": "SUCCESS"
        }
    }
}
```

