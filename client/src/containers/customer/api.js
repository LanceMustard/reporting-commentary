import axios from 'axios'

import { ROOT_URL } from 'actions/environment'

const API = 'api/Customers';

export function fetchCustomers() {
  const request = axios.get(`${ROOT_URL}${API}`)
  return request
}

export function fetchCustomer(id) {
  const request = axios.get(`${ROOT_URL}${API}/${id}`)
  return request
}

export function createCustomer(props) {
  const request = axios.post(`${ROOT_URL}${API}`, props)
  return request
}

export function updateCustomer(props) {
  const request = axios.put(`${ROOT_URL}${API}/${props.id}`, props)
  return request
}

export function deleteCustomer(id) {
  const request = axios.delete(`${ROOT_URL}${API}/${id}`)
  return request
}