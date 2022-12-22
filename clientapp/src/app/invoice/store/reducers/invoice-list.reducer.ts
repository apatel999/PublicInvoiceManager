import * as fromInvoice from '../actions/invoice-list.action';
import { Order } from '../../order.model';

export interface InvoiceListState {
    data: Order[],
    loaded: boolean,
    loading: boolean,
    creating: boolean,
    created: boolean,
    createFailed: boolean
}

export const initialInvoiceListState: InvoiceListState = {
    data: [],
    loaded: false,
    loading: false,
    creating: false,
    created: false,
    createFailed: false
}

export function reducer(state = initialInvoiceListState, action: fromInvoice.InvoiceListAction): InvoiceListState {

    switch (action.type) {
        case fromInvoice.LOAD_INVOICE_LIST: {
            return {
                ...state,
                loading: true
            };
        }
        case fromInvoice.LOAD_INVOICE_LIST_SUCCESS: {
            return {
                ...state,
                data: action.payload,
                loading: false,
                loaded: true
            };
        }

        case fromInvoice.LOAD_INVOICE_LIST_FAIL: {
            return {
                ...state,
                loading: false,
                loaded: false
            };
        }

        case fromInvoice.INIT_CREATE_WEEKLY_INVOICES: {
            return {
                ...state,
                creating: false,
                created: false,
                createFailed: false
            }
        }

        case fromInvoice.CREATE_WEEKLY_INVOICES: {
            return {
                ...state,
                creating: true,
                created: false,
                createFailed: false
            };
        }

        case fromInvoice.CREATE_WEEKLY_INVOICES_SUCCESS: {

            return {
                ...state,
                creating: false,
                created: true,
                createFailed: false
            };
        }

        case fromInvoice.CREATE_WEEKLY_INVOICES_FAIL: {

            return {
                ...state,
                creating: false,
                created: false,
                createFailed: true
            };
        }
    }

    return state;
}