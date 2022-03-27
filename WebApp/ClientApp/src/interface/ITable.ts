
export interface ITable<T> {
    data: Array<T>,
    pageNumber: number,
    rowsOfPage: number,
    totalPages?: number,
    totalData?: number
}

export interface IHeader {
    text: string,
    value: string,
    align: string,
    sortable: boolean
}

export interface VPagination {
    page: number,
    itemsPerPage: number
}